using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomDoor : DoorBase
{
    [SerializeField] Tilemap _leftMap;
    [SerializeField] Tilemap _rightMap;
    [SerializeField] Animator _animator;

    #region DoorBase
    protected override void Interact()
    {
        CheckOpen();
    }
    #endregion

    void CheckOpen()
    {
        if (!_collider.isTrigger)
            Open();
        else
            OnInteract();
    }

    void Open()
    {
        if (_animator.GetBool("isOpen"))
            return;
        _animator.SetBool("isOpen", true);
    }

    #region Animation Event
    void EndOpen()
    {
        _collider.isTrigger = true;
    }
    #endregion

    #region Interface
    public override void OnInteract()
    {
        base.OnInteract();
        _cameraManager.UpdateTileBound(_leftMap, _rightMap);
    }

    public override void OnLoopEvent()
    {
        base.OnLoopEvent();
        _animator.SetBool("isOpen", false);
    }
    #endregion
}