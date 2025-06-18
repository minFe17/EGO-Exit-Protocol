using UnityEngine;
using UnityEngine.Rendering.Universal;
using Utils;

public class MainCamera : MonoBehaviour
{
    PixelPerfectCamera _camera;
    Transform _target;
    CameraManager _cameraManager;

    float _halfWidth;

    public void Init()
    {
        _camera = GetComponent<PixelPerfectCamera>();
        _target = GenericSingleton<PlayerManager>.Instance.Player.transform;
        _cameraManager = GenericSingleton<CameraManager>.Instance;
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

        Vector3 movePos = new Vector3(clamp, _cameraManager.CameraYPos, transform.position.z);

        movePos = _camera.RoundToPixel(movePos);
        transform.position = movePos;
    }
}