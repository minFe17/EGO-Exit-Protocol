using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class Camera : MonoBehaviour
{
    [SerializeField] PixelPerfectCamera _camera;
    [SerializeField] Transform _target;
    [SerializeField] Tilemap _tilemap;

    Vector2 _minBounds;
    Vector2 _maxBounds;
    float _halfWidth;

    void Start()
    {
        Bounds bounds = _tilemap.localBounds;
        _minBounds = bounds.min;
        _maxBounds = bounds.max;
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

        float clamp = Mathf.Clamp(targetPos.x, _minBounds.x + _halfWidth, _maxBounds.x - _halfWidth);

        Vector3 movePos = new Vector3(clamp, transform.position.y, transform.position.z);

        movePos = _camera.RoundToPixel(movePos);
        transform.position = movePos;
    }
}