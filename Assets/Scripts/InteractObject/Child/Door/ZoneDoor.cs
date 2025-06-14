using UnityEngine;
using UnityEngine.Tilemaps;

public class ZoneDoor : DoorBase
{
    [SerializeField] Tilemap _targetMap;
    [SerializeField] Transform _targetPos;

    protected override void Init()
    {
        base.Init();
    }

    public override void OnInteract()
    {
        // 문 사용(들어감) 대사 처리
        _playerManager.SetPlayerPosition(_targetPos.position);
        _cameraManager.UpdateTileBound(_targetMap);
    }
}