using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class MainCamera : MonoBehaviour
{
    [SerializeField] PixelPerfectCamera _camera;
    [SerializeField] Transform _target;
    [SerializeField] Tilemap _tilemap;

    Vector3 _minBounds;
    Vector3 _maxBounds;
    float _halfWidth;

    void Start()
    {
        TileBound();
    }

    void LateUpdate()
    {
        Move();
    }

    void TileBound()
    {
        BoundsInt bounds = _tilemap.cellBounds;
        int actualMinX = GetActualMinX(bounds);
        float cellHalf = _tilemap.cellSize.x / 2f;

        _minBounds = _tilemap.GetCellCenterWorld(new Vector3Int(actualMinX, 0, 0)) - new Vector3(cellHalf, cellHalf);
        _maxBounds = _tilemap.GetCellCenterWorld(bounds.max) - new Vector3(cellHalf, cellHalf);
    }

    int GetActualMinX(BoundsInt bounds)
    {
        int minX = int.MaxValue;
        bool found = false;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (_tilemap.HasTile(pos))
            {
                if (pos.x < minX)
                    minX = pos.x;
                found = true;
            }
        }

        if (!found)
            return bounds.min.x;

        return minX;
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