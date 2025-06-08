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
    Vector3 _cellSize;
    float _halfWidth;
    float _cellHalf;

    void Start()
    {
        _cellHalf = _tilemap.cellSize.x / 2f;
        _cellSize = new Vector3(_cellHalf, _cellHalf);
        UpdateTileBound();
    }

    void LateUpdate()
    {
        Move();
    }

    void UpdateTileBound()
    {
        int min = GetLeftTilePos(_tilemap);
        int max = GetRightTilePos(_tilemap);
        Vector3 currentMinWorld = _tilemap.GetCellCenterWorld(new Vector3Int(min, 0, 0));
        Vector3 currentMaxWorld = _tilemap.GetCellCenterWorld(new Vector3Int(max, 0, 0));

        _minBounds = currentMinWorld - _cellSize;
        _maxBounds = currentMaxWorld - _cellSize;
    }

    int GetLeftTilePos(Tilemap tilemap)
    {
        int minX = int.MaxValue;
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos) && pos.x < minX)
                minX = pos.x;
        }
        return minX;
    }

    int GetRightTilePos(Tilemap tilemap)
    {
        int maxX = int.MinValue;
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos) && pos.x > maxX)
                maxX = pos.x;
        }
        return maxX;
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