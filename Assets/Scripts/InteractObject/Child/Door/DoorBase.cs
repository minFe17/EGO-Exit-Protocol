using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DoorBase : MonoBehaviour, IInteractable, ILoopObject, IDoorEvent
{
    [SerializeField] protected BoxCollider2D _collider;
    [SerializeField] DoorMemento _doorMemento;
    
    protected CameraManager _cameraManager;

    List<IDoorEvent> _doorObserve = new List<IDoorEvent>();
    ObserveManager _observerManager;
    InteractObjectManager _interactObjectManager;
    bool _currentLock;

    void Start()
    {
        _doorObserve.Add(this);
        Init();
        OnLoopEvent();
    }

    protected virtual void Init()
    {
        _observerManager = GenericSingleton<ObserveManager>.Instance;
        _cameraManager = GenericSingleton<CameraManager>.Instance;
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _interactObjectManager.SetInteractable(gameObject, this);
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        GenericSingleton<InteractObjectManager>.Instance.SetInteractable(gameObject, this);
    }

    protected virtual void Interact()
    {
        OnInteract();
    }

    void TryUnlock()
    {
        // 문이 잠겼다는 대사 처리
        // 플레이어한테 키가 있는지 체크
        //OnOpen();
        //OnOpenFail();
    }

    void IInteractable.Interact()
    {
        if (_currentLock)
            TryUnlock();
        else
            Interact();
    }

    public virtual void OnLoopEvent()
    {
        _currentLock = _doorMemento.IsLock;
        _collider.isTrigger = _doorMemento.IsTrigger;
    }

    #region Interface
    public void OnUnlock()
    {
        _currentLock = false;
        _observerManager.DoorObserve.OnUnlockEvent();
        Interact();
    }

    public void OnUnlockFail()
    {
        _observerManager.DoorObserve.OnUnlockFailEvent();
    }

    public virtual void OnInteract()
    {
        _observerManager.DoorObserve.OnInteractEvent();
    }

    GameObject IInteractable.GetGameObject()
    {
        return gameObject;
    }
    #endregion
}