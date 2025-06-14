using UnityEngine;
using Utils;

public class LoopManager : MonoBehaviour, IMediatorEvent
{
    // ╫л╠шео
    ObserveManager _observeManager;

    int _loopCount;

    public void Init()
    {
        _observeManager = GenericSingleton<ObserveManager>.Instance;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.LoopEvent, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _loopCount++;
        _observeManager.LoopObserve.OnLoopEvent();
    }
}