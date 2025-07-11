using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZoneDoor : DoorBase
{
    [SerializeField] Tilemap _targetMap;
    [SerializeField] Transform _targetPos;
    [SerializeField] bool _isTrapRoom;
    [SerializeField] bool _isMemory;

    [ShowIf("_isTrapRoom")]
    [SerializeField] Vector3 _researcherSpawnPosition;

    [ShowIf("_isMemory")]
    [SerializeField] EMemoryType _memoryType;

    protected override void Init()
    {
        base.Init();
    }

    public override void OnInteract()
    {
        if (_isMemory)
            _mediatorManager.Notify(EMediatorEventType.AddMemory, _memoryType);
        _playerManager.SetPlayerPosition(_targetPos.position);
        _cameraManager.UpdateTileBound(_targetMap);
        _mediatorManager.Notify(EMediatorEventType.PlayerLocationChanged, _targetPos.position);
        if (_isTrapRoom)
            _mediatorManager.Notify(EMediatorEventType.SpawnResearcher, _researcherSpawnPosition);
    }
}