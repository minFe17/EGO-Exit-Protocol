using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class ZoneDoor : DoorBase
{
    [SerializeField] Tilemap _targetMap;
    [SerializeField] Transform _targetPos;

    PlayerManager _playerManager;

    protected override void Init()
    {
        base.Init();
        _playerManager = GenericSingleton<PlayerManager>.Instance;
    }

    public override void OnInteract()
    {
        base.OnInteract();
        _playerManager.SetPlayerPosition(_targetPos.position);
        _cameraManager.UpdateTileBound(_targetMap);
    }
}