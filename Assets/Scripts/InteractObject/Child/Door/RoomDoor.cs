using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomDoor : DoorBase
{
    [ShowIf("IsNotTrapDoor")]
    [SerializeField] Tilemap _leftMap;

    [ShowIf("IsNotTrapDoor")]
    [SerializeField] Tilemap _rightMap;

    [ShowIf("IsNotTrapDoor")]
    [SerializeField] Animator _animator;

    #region DoorBase
    protected override void InteractDoor()
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

    public override void OnInteract()
    {
        // �� ���(��) ��� ó��
        _cameraManager.UpdateTileBound(_leftMap, _rightMap);
    }

    #region Animation Event
    void EndOpen()
    {
        // ���ȴٴ� ��� ó��?
        _collider.isTrigger = true;
    }
    #endregion

    #region Interface
    public override void OnLoopEvent()
    {
        base.OnLoopEvent();
        if (_animator != null)
            _animator.SetBool("isOpen", false);
    }
    #endregion
}