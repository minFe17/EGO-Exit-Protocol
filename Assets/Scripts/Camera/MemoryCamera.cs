using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class MemoryCamera : MonoBehaviour
{
    [SerializeField] Camera _captureCamera;
    [SerializeField] RenderTexture _captureTexture;

    Texture2D _texture;

    void Start()
    {
        _texture = new Texture2D(_captureTexture.width, _captureTexture.height, TextureFormat.RGBA32, false);
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
}