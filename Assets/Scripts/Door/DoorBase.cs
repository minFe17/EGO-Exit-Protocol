using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DoorBase : MonoBehaviour, IInteractable, ILoopObject, IDoorEvent
{
    [SerializeField] BoxCollider2D _collider;
    [SerializeField] DoorMemento _doorMemento;

    List<IDoorEvent> _doorObserve = new List<IDoorEvent>();
    ObserveManager _observerManager;
    bool _currentLock;

    void Start()
    {
        _doorObserve.Add(this);
        _observerManager = GenericSingleton<ObserveManager>.Instance;
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        GenericSingleton<InteractObjectManager>.Instance.SetInteractable(gameObject, this);
        OnLoopEvent();
    }

    void IInteractable.Interact()
    {
        if (_currentLock)
        {
            // 플레이어한테 키가 있는지 체크
            //OnOpen();
            //OnOpenFail();
        }
        else
            OnInteract();
    }

    public void OnLoopEvent()
    {
        _currentLock = _doorMemento.IsLock;
        _collider.isTrigger = _doorMemento.IsTrigger;
    }

    #region DoorEvent interface
    public void OnOpen()
    {
        _currentLock = false;
        _collider.isTrigger = true;
        _observerManager.DoorObserve.OnOpenEvent();
    }

    public void OnOpenFail()
    {
        _observerManager.DoorObserve.OnOpenFailEvent();
    }

    public virtual void OnInteract()
    {
        _observerManager.DoorObserve.OnInteractEvent();
    }
    #endregion
}