using UnityEngine;
using Utils;

public class LoopManager : MonoBehaviour, IMediatorEvent
{
    // ╫л╠шео
    ObserveManager _observeManager;
    MediatorManager _mediatorManager;
    int _loopCount;

    public void Init()
    {
        _observeManager = GenericSingleton<ObserveManager>.Instance;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.LoopEvent, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _loopCount++;
        _observeManager.LoopObserve.OnLoopEvent();
        _mediatorManager.Notify(EMediatorEventType.ChangeLoopCount, _loopCount);
    }
}