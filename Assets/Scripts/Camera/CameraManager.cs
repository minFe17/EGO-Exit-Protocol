using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class CameraManager : MonoBehaviour, ILoopObject
{
    // ╫л╠шео
    CameraMemento _cameraMemento = new CameraMemento();

    Vector3 _cellSize = Vector3.zero;
    Vector3 _minBounds;
    Vector3 _maxBounds;

    public CameraMemento CameraMemento { get => _cameraMemento; }
    public Vector3 MinBounds { get => _minBounds; }
    public Vector3 MaxBounds { get => _maxBounds; }

    public void Init()
    {
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        UpdateTileBound(_cameraMemento.LoopTilemap);
    }

    void SetCellSize(Tilemap tilemap)
    {
        float cellHalf = tilemap.cellSize.x / 2f;
        _cellSize = new Vector3(cellHalf, cellHalf);
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

    public void UpdateTileBound(Tilemap tilemap)
    {
        int min = GetLeftTilePos(tilemap);
        int max = GetRightTilePos(tilemap);
        Vector3 currentMinWorld = tilemap.GetCellCenterWorld(new Vector3Int(min, 0, 0));
        Vector3 currentMaxWorld = tilemap.GetCellCenterWorld(new Vector3Int(max, 0, 0));

        if (_cellSize == Vector3.zero)
            SetCellSize(tilemap);
        _minBounds = currentMinWorld - _cellSize;
        _maxBounds = currentMaxWorld - _cellSize;
    }

    void ILoopObject.OnLoopEvent()
    {
        UpdateTileBound(_cameraMemento.LoopTilemap);
    }
}