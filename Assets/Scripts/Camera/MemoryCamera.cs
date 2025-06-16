using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class MemoryCamera : MonoBehaviour
{
    [SerializeField] Camera _captureCamera;
    [SerializeField] RenderTexture _captureTexture;

    Texture2D _texture;

    private void Start()
    {
        _texture = new Texture2D(_captureTexture.width, _captureTexture.height, TextureFormat.RGBA32, false);
        Capture();
    }

    public void Capture()
    {
        _captureCamera.targetTexture = _captureTexture;
        _captureCamera.allowHDR = false;
        _captureCamera.Render();

        AsyncGPUReadback.Request(_captureTexture, 0, TextureFormat.RGBA32, OnCompleteReadback);
    }

    void OnCompleteReadback(AsyncGPUReadbackRequest request)
    {
        _texture.LoadRawTextureData(request.GetData<byte>());
        _texture.Apply();

        byte[] pngData = _texture.EncodeToPNG();
        string path = Path.Combine(Application.persistentDataPath, "savedSprite.png");
        File.WriteAllBytes(path, pngData);
        Debug.Log("URP 캡처 저장 완료: " + path);

        _captureCamera.targetTexture = null;
        RenderTexture.active = null;
    }
}
