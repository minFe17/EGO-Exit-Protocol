using UnityEngine;
using Utils;

public class LoopManager : MonoBehaviour, IMediatorEvent
{
    // ╫л╠шео
    ObserveManager _observeManager;
    MediatorManager _mediatorManager;

    public void Init()
    {
        _observeManager = GenericSingleton<ObserveManager>.Instance;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.LoopEvent, this);
        GenericSingleton<JsonManager>.Instance.ReadData.ReadLoopData();
        _mediatorManager.Notify(EMediatorEventType.ChangeLoopCount);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        DataSingleton<LoopData>.Instance.AddLoopCount();
        _observeManager.LoopObserve.OnLoopEvent();
        _mediatorManager.Notify(EMediatorEventType.ChangeLoopCount);
        GenericSingleton<JsonManager>.Instance.WriteData.WriteLoopData();
    }
}