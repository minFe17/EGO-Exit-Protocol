using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using Utils;

public class MemoryCamera : MonoBehaviour, IMediatorEvent
{
    [SerializeField] Camera _captureCamera;
    [SerializeField] RenderTexture _captureTexture;
    [SerializeField] Vector3 _startPos;

    Queue<MemoryData> _memoryDataQueue = new Queue<MemoryData>();
    MemoryData _currentMemoryData;

    Transform _target;
    Texture2D _texture;
    MediatorManager _mediatorManager;
    CameraManager _cameraManager;

    Rect _rect;
    Vector2 _pivot;

    float _halfWidth;
    bool _endCapture = true;

    void Start()
    {
        _texture = new Texture2D(_captureTexture.width, _captureTexture.height, TextureFormat.RGBA32, false);
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.NeedCapture, this);
        _cameraManager = GenericSingleton<CameraManager>.Instance;
        _rect = new Rect(0, 0, _texture.width, _texture.height);
        _pivot = new Vector2(0.5f, 0.5f);
        _target = GenericSingleton<PlayerManager>.Instance.Player.transform;
    }

    private void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        float cameraHalfHeight = _captureCamera.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight / _captureCamera.aspect;

        _halfWidth = cameraHalfWidth;

        ClampCameraPos();
    }

    void ClampCameraPos()
    {
        Vector3 targetPos = _target.position + _startPos;
        float clamp = Mathf.Clamp(targetPos.x, _cameraManager.MinBounds.x + _halfWidth, _cameraManager.MaxBounds.x - _halfWidth);

        Vector3 movePos = new Vector3(clamp, targetPos.y, transform.position.z);
        transform.position = movePos;
    }

    void Capture()
    {
        if (!_endCapture || _memoryDataQueue.Count == 0)
            return;

        _currentMemoryData = _memoryDataQueue.Dequeue();
        _endCapture = false;

        RenderTexture.active = _captureTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;

        _captureCamera.targetTexture = _captureTexture;
        _captureCamera.allowHDR = false;
        _captureCamera.Render();

        AsyncGPUReadback.Request(_captureTexture, 0, TextureFormat.RGBA32, OnCaptureComplete);
    }

    void OnCaptureComplete(AsyncGPUReadbackRequest request)
    {
        if (_currentMemoryData == null)
        {
            _endCapture = true;
            return;
        }

        _texture = new Texture2D(_captureTexture.width, _captureTexture.height, TextureFormat.RGBA32, false);
        _texture.LoadRawTextureData(request.GetData<byte>());
        _texture.Apply();

        byte[] pngData = _texture.EncodeToPNG();

        File.WriteAllBytes(_currentMemoryData.SpritePath, pngData);
        Sprite capturedSprite = Sprite.Create(_texture, _rect, _pivot);
        _currentMemoryData.Sprite = capturedSprite;

        _captureCamera.targetTexture = null;
        RenderTexture.active = null;
        _currentMemoryData = null;
        _endCapture = true;

        Capture();
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        MemoryData memoryData = (MemoryData)data;
        if (File.Exists(memoryData.SpritePath))
            return;
        _memoryDataQueue.Enqueue((MemoryData)data);
        if (_endCapture)
            Capture();
    }
}