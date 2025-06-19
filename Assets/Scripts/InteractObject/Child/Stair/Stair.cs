using NaughtyAttributes;
using UnityEngine;
using Utils;

public class Stair : MonoBehaviour, IInteractable
{
    [SerializeField] bool _isRooftopStair;

    [ShowIf("IsNotRooftop")]
    [SerializeField] StairSetting _stairSetting;

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

    #region NaughtyAttributes
    bool IsNotRooftop() => !_isRooftopStair;
    #endregion

    #region Interface
    void IInteractable.Interact()
    {
        if(_isRooftopStair)
        {
            // 기억 추가
            _mediatorManager.Notify(EMediatorEventType.AddMemory, EMemoryType.DontGoRooftop);
            // 대사 후 적 소환?
            return;
        }
        _playerManager.SetPlayerPosition(_stairSetting.TargetPos.position);
        _cameraManager.UpdateTileBound(_stairSetting.TargetTilemap);
        _cameraManager.SetCameraPosition(_stairSetting.CameraPosY);
        _mediatorManager.Notify(EMediatorEventType.PlayerLocationChanged, _stairSetting.TargetPos.position);
    }

    GameObject IInteractable.GetGameObject()
    {
        return gameObject;
    }
    #endregion
}