using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class MainCamera : MonoBehaviour
{
    [SerializeField] PixelPerfectCamera _camera;
    [SerializeField] Transform _target;
    [SerializeField] Tilemap _currentTilemap;
    [SerializeField] Tilemap _nextTileMap;

    Vector3 _minBounds;
    Vector3 _maxBounds;
    Vector3 _cellSize;
    float _halfWidth;
    float _cellHalf;

    void Start()
    {
        _cellHalf = _currentTilemap.cellSize.x / 2f;
        _cellSize = new Vector3(_cellHalf, _cellHalf);
        UpdateTileBound();
    }

    void LateUpdate()
    {
        Move();
    }

    void UpdateTileBound()
    {
        int currentMin = GetLeftTilePos(_currentTilemap);
        int currentMax = GetRightTilePos(_currentTilemap);
        Vector3 currentMinWorld = _currentTilemap.GetCellCenterWorld(new Vector3Int(currentMin, 0, 0));
        Vector3 currentMaxWorld = _currentTilemap.GetCellCenterWorld(new Vector3Int(currentMax, 0, 0));

        if (_nextTileMap != null)
        {
            int nextMin = GetLeftTilePos(_nextTileMap);
            int nextMax = GetRightTilePos(_nextTileMap);
            Vector3 nextMinWorld = _nextTileMap.GetCellCenterWorld(new Vector3Int(nextMin, 0, 0));
            Vector3 nextMaxWorld = _nextTileMap.GetCellCenterWorld(new Vector3Int(nextMax, 0, 0));

            bool currentIsLeft = currentMin <= nextMin;
            _minBounds = (currentIsLeft ? currentMinWorld : nextMinWorld) - _cellSize;
            _maxBounds = (currentIsLeft ? nextMaxWorld : currentMaxWorld) - _cellSize;
        }
        else
        {
            _minBounds = _currentTilemap.GetCellCenterWorld(new Vector3Int(currentMin, 0, 0)) - _cellSize;
            _maxBounds = _currentTilemap.GetCellCenterWorld(new Vector3Int(currentMax, 0, 0)) - _cellSize;
        }
    }

    int GetLeftTilePos(Tilemap tilemap)
    {
        int minX = int.MaxValue;
        foreach(Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if(tilemap.HasTile(pos) && pos.x < minX)
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