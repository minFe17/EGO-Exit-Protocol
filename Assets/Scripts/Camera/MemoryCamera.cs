using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using Utils;

public class MemoryCamera : MonoBehaviour, IMediatorEvent
{
    [SerializeField] Camera _captureCamera;
    [SerializeField] RenderTexture _captureTexture;

    Texture2D _texture;
    MediatorManager _mediatorManager;

    MemoryData _memoryData;

    Rect _rect;
    Vector2 _pivot;

    void Start()
    {
        _texture = new Texture2D(_captureTexture.width, _captureTexture.height, TextureFormat.RGBA32, false);
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.NeedCapture, this);
        _rect = new Rect(0, 0, _texture.width, _texture.height);
        _pivot = new Vector2(0.5f, 0.5f);
    }

    void Capture()
    {
        // 캡처한 파일이 있으면 (경로에 파일이 있으면) 안함
        _captureCamera.targetTexture = _captureTexture;
        _captureCamera.allowHDR = false;
        _captureCamera.Render();

        AsyncGPUReadback.Request(_captureTexture, 0, TextureFormat.RGBA32, OnCaptureComplete);
    }

    void OnCaptureComplete(AsyncGPUReadbackRequest request)
    {
        _texture.LoadRawTextureData(request.GetData<byte>());
        _texture.Apply();

        byte[] pngData = _texture.EncodeToPNG();

        // path 경로 combine은 memoryData에서
        string path = Path.Combine(Application.persistentDataPath, "SaveData.png");

        File.WriteAllBytes(_memoryData.SpritePath, pngData);
        Sprite capturedSprite = Sprite.Create(_texture, _rect, _pivot);
        _memoryData.Sprite = capturedSprite;

        _captureCamera.targetTexture = null;
        RenderTexture.active = null;
        _memoryData = null;
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _memoryData = (MemoryData)data;
        Capture();
    }
}