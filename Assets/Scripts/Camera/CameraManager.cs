using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

/// <summary>
/// 타일맵을 기준으로 카메라 경계 및 위치를 설정
/// </summary>
public class CameraManager : MonoBehaviour, ILoopObject
{
    // 싱글턴
    MementoManager _mementoManager;
    PrefabManager _prefabManager;
    Tilemap _currentTilemap;
    Vector3 _cellSize = Vector3.zero;
    Vector3 _minBounds;
    Vector3 _maxBounds;
    float _cameraYPos;

    public Vector3 MinBounds { get => _minBounds; }
    public Vector3 MaxBounds { get => _maxBounds; }
    public float CameraYPos { get => _cameraYPos; }

    public void Init()
    {
        _prefabManager = GenericSingleton<PrefabManager>.Instance;
        if (_mementoManager == null)
            _mementoManager = GenericSingleton<MementoManager>.Instance;
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        CreateCamera();
        UpdateTileBound(_mementoManager.CameraMemento.LoopTilemap);
        SetCameraPosition(0);
    }

    void CreateCamera()
    {
        Instantiate(_prefabManager.GetPrefabLoad(EPrefabType.Camera).GetPrefab(ECameraPrefabType.MainCamera));
        Instantiate(_prefabManager.GetPrefabLoad(EPrefabType.Camera).GetPrefab(ECameraPrefabType.MemoryCamera));
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

    /// <summary>
    /// 타일맵의 좌/우 경계로부터 카메라 이동 범위 계산
    /// </summary>
    public void UpdateTileBound(Tilemap tilemap)
    {
        _currentTilemap = tilemap;

        // 타일이 있는 가장 왼쪽, 오른쪽 셀 위치를 찾음
        int min = GetLeftTilePos(_currentTilemap);
        int max = GetRightTilePos(_currentTilemap);

        // 셀 중심 위치를 월드 좌표로 변환
        Vector3 currentMinWorld = _currentTilemap.GetCellCenterWorld(new Vector3Int(min, 0, 0));
        Vector3 currentMaxWorld = _currentTilemap.GetCellCenterWorld(new Vector3Int(max, 0, 0));

        // 셀 크기 정보가 없을 경우 설정
        if (_cellSize == Vector3.zero)
            SetCellSize(_currentTilemap);

        // 경계 보정 후 저장
        _minBounds = currentMinWorld - _cellSize;
        _maxBounds = currentMaxWorld + _cellSize;
    }

    public void UpdateTileBound(Tilemap leftmap, Tilemap rightmap)
    {
        if (_currentTilemap != leftmap)
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
        UpdateTileBound(_mementoManager.CameraMemento.LoopTilemap);
        SetCameraPosition(0);
    }
}