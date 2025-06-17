using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class Stair : MonoBehaviour, IInteractable
{
    [SerializeField] Tilemap _targetTilemap;
    [SerializeField] Transform _targetPos;
    [SerializeField] float _cameraPosY;

    InteractObjectManager _interactObjectManager;
    PlayerManager _playerManager;
    CameraManager _cameraManager;
    MediatorManager _mediatorManager;

    void Start()
    {
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _interactObjectManager.SetInteractable(gameObject, this);
        _playerManager = GenericSingleton<PlayerManager>.Instance;
        _cameraManager = GenericSingleton<CameraManager>.Instance;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
    }

    void IInteractable.Interact()
    {
        _playerManager.SetPlayerPosition(_targetPos.position);
        _cameraManager.UpdateTileBound(_targetTilemap);
        _cameraManager.SetCameraPosition(_cameraPosY);
        _mediatorManager.Notify(EMediatorEventType.PlayerLocationChanged, _targetPos.position);
    }

    GameObject IInteractable.GetGameObject()
    {
        return gameObject;
    }
}