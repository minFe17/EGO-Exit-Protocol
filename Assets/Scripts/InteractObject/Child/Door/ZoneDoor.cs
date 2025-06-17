using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class ZoneDoor : DoorBase
{
    [SerializeField] Tilemap _targetMap;
    [SerializeField] Transform _targetPos;

    MediatorManager _mediatorManager;

    protected override void Init()
    {
        base.Init();
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;

    }

    public override void OnInteract()
    {
        // 문 사용(들어감) 대사 처리
        _playerManager.SetPlayerPosition(_targetPos.position);
        _cameraManager.UpdateTileBound(_targetMap);
        _mediatorManager.Notify(EMediatorEventType.PlayerLocationChanged, _targetPos.position);
    }
}