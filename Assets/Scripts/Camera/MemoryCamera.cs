using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using Utils;

/// <summary>
/// 게임 내 특정 장면을 캡처하는 카메라
/// RenderTexture를 통해 이미지로 저장하고, GPU Readback으로 처리
/// </summary>
public class MemoryCamera : MonoBehaviour, IMediatorEvent
{
    [SerializeField] RenderTexture _captureTexture;
    [SerializeField] Vector3 _startPos;

    Queue<MemoryData> _memoryDataQueue = new Queue<MemoryData>();
    MemoryData _currentMemoryData;
    Camera _captureCamera;

    Transform _target;
    Texture2D _texture;
    CameraManager _cameraManager;

    Rect _rect;
    Vector2 _pivot;

    float _halfWidth;
    bool _endCapture = true;

    #region Unity LifeCycle
    void Start()
    {
        _captureCamera = GetComponent<Camera>();

        // 텍스처 초기화
        _texture = new Texture2D(_captureTexture.width, _captureTexture.height, TextureFormat.RGBA32, false);

        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.NeedCapture, this);

        _cameraManager = GenericSingleton<CameraManager>.Instance;

        // Sprite 생성을 위한 Rect, Pivot 설정
        _rect = new Rect(0, 0, _texture.width, _texture.height);
        _pivot = new Vector2(0.5f, 0.5f);

        _target = GenericSingleton<PlayerManager>.Instance.Player.transform;
    }

    void LateUpdate()
    {
        Move();
    }
    #endregion

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

    /// <summary>
    /// Queue에서 메모리 데이터를 꺼내 RenderTexture로 캡처 시작
    /// GPU 리드백을 비동기적으로 요청
    /// </summary>
    void Capture()
    {
        // 현재 캡처 중이거나 캡처할 데이터가 없으면 중단
        if (!_endCapture || _memoryDataQueue.Count == 0)
            return;

        // Queue에서 다음 캡처 데이터 꺼냄
        _currentMemoryData = _memoryDataQueue.Dequeue();
        _endCapture = false;

        // 캡처 전에 RenderTexture 초기화
        RenderTexture.active = _captureTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;

        // 카메라에 캡처용 RenderTexture 지정 후 렌더링 실행
        _captureCamera.targetTexture = _captureTexture;
        _captureCamera.allowHDR = false;
        _captureCamera.Render();

        // GPU 메모리로부터 비동기적으로 이미지 데이터 요청
        AsyncGPUReadback.Request(_captureTexture, 0, TextureFormat.RGBA32, OnCaptureComplete);
    }

    /// <summary>
    /// GPU 캡처가 완료되었을 때 호출되는 콜백
    /// 텍스처 데이터를 파일로 저장하고 Sprite로 생성하여 MemoryData에 저장
    /// 이후 다음 캡처 진행
    /// </summary>
    /// <param name="request">GPU 리드백 요청 결과</param>
    void OnCaptureComplete(AsyncGPUReadbackRequest request)
    {
        // 캡처 대상이 null일 경우 종료
        if (_currentMemoryData == null)
        {
            _endCapture = true;
            return;
        }

        // GPU에서 받은 데이터로 텍스처 생성
        _texture = new Texture2D(_captureTexture.width, _captureTexture.height, TextureFormat.RGBA32, false);
        _texture.LoadRawTextureData(request.GetData<byte>());
        _texture.Apply();

        // PNG로 인코딩 후 파일 저장
        byte[] pngData = _texture.EncodeToPNG();

        // Sprite로 변환해 메모리 데이터에 저장
        File.WriteAllBytes(_currentMemoryData.SpritePath, pngData);
        Sprite capturedSprite = Sprite.Create(_texture, _rect, _pivot);
        _currentMemoryData.Sprite = capturedSprite;

        // 카메라와 렌더링 상태 초기화
        _captureCamera.targetTexture = null;
        RenderTexture.active = null;
        _currentMemoryData = null;
        _endCapture = true;

        // Queue에 남아있는 캡처가 있다면 다음 캡처 진행
        Capture();
    }

    /// <summary>
    /// 중재자 이벤트 수신 처리
    /// 전달받은 MemoryData가 아직 캡처되지 않은 경우 Queue에 등록 후 캡처 실행
    /// </summary>
    void IMediatorEvent.HandleEvent(object data)
    {
        MemoryData memoryData = (MemoryData)data;

        // 이미 해당 이미지가 파일로 존재하면 return
        if (File.Exists(memoryData.SpritePath))
            return;

        // Queue에 등록 후, 현재 캡처 중이 아니라면 즉시 캡처 시작
        _memoryDataQueue.Enqueue((MemoryData)data);
        if (_endCapture)
            Capture();
    }
}