using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class CameraManager : MonoBehaviour, ILoopObject
{
    // ╫л╠шео
    CameraMemento _cameraMemento = new CameraMemento();
    Tilemap _currentTilemap;
    Vector3 _cellSize = Vector3.zero;
    Vector3 _minBounds;
    Vector3 _maxBounds;
    float _cameraYPos;

    public CameraMemento CameraMemento { get => _cameraMemento; }
    public Vector3 MinBounds { get => _minBounds; }
    public Vector3 MaxBounds { get => _maxBounds; }
    public float CameraYPos { get => _cameraYPos; }

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
        _currentTilemap = tilemap;
        int min = GetLeftTilePos(_currentTilemap);
        int max = GetRightTilePos(_currentTilemap);
        Vector3 currentMinWorld = _currentTilemap.GetCellCenterWorld(new Vector3Int(min, 0, 0));
        Vector3 currentMaxWorld = _currentTilemap.GetCellCenterWorld(new Vector3Int(max, 0, 0));

        if (_cellSize == Vector3.zero)
            SetCellSize(_currentTilemap);
        _minBounds = currentMinWorld - _cellSize;
        _maxBounds = currentMaxWorld + _cellSize;
    }

    public void UpdateTileBound(Tilemap leftmap,  Tilemap rightmap)
    {
        if(_currentTilemap != leftmap)
            UpdateTileBound(leftmap);
        else
            UpdateTileBound(rightmap);
    }

    public void SetCameraPosition(float pos)
    {
        _cameraYPos = pos;
    }

    void ILoopObject.OnLoopEvent()
    {
        UpdateTileBound(_cameraMemento.LoopTilemap);
    }
}