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

    void Start()
    {
        _texture = new Texture2D(_captureTexture.width, _captureTexture.height, TextureFormat.RGBA32, false);
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
    }

    void OnCaptureComplete(AsyncGPUReadbackRequest request)
    {
        _texture.LoadRawTextureData(request.GetData<byte>());
        _texture.Apply();

        byte[] pngData = _texture.EncodeToPNG();
        // 파일 이름은 기억 조각에 따라서 바꾸기
        string path = Path.Combine(Application.persistentDataPath, "savedSprite.png");
        File.WriteAllBytes(path, pngData);

        _captureCamera.targetTexture = null;
        RenderTexture.active = null;
    }

    public void Capture()
    {
        _captureCamera.targetTexture = _captureTexture;
        _captureCamera.allowHDR = false;
        _captureCamera.Render();

        AsyncGPUReadback.Request(_captureTexture, 0, TextureFormat.RGBA32, OnCaptureComplete);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        MemoryData memoryData = (MemoryData)data;
    }
}