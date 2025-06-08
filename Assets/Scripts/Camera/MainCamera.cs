using UnityEngine;
using UnityEngine.Rendering.Universal;
using Utils;

public class MainCamera : MonoBehaviour
{
    [SerializeField] PixelPerfectCamera _camera;
    [SerializeField] Transform _target;

    CameraManager _cameraManager;
    float _halfWidth;

    void Start()
    {
        _cameraManager = GenericSingleton<CameraManager>.Instance;
        _cameraManager.Init();
    }

    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        float cameraSize = _camera.refResolutionY / (2f * _camera.assetsPPU);
        float aspect = (float)Screen.width / Screen.height;
        _halfWidth = cameraSize * aspect;

        Vector3 targetPos = _target.position;

        float clamp = Mathf.Clamp(targetPos.x, _cameraManager.MinBounds.x + _halfWidth, _cameraManager.MaxBounds.x - _halfWidth);

        Vector3 movePos = new Vector3(clamp, transform.position.y, transform.position.z);

        movePos = _camera.RoundToPixel(movePos);
        transform.position = movePos;
    }
}