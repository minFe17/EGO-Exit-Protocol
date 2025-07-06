using UnityEngine;
using Utils;

public class EndingDoor : DoorBase
{
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
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.StartEndingFade, EEndingType.Door);
    }

    #region Animation Event
    void EndOpen()
    {
        // 열렸다는 대사 처리?
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