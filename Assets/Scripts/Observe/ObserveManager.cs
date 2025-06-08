using UnityEngine;

public class ObserveManager : MonoBehaviour
{
    // �̱���
    LoopObserve _loopObserve = new LoopObserve();
    DoorObserve _doorObserve = new DoorObserve();

    public DoorObserve DoorObserve { get => _doorObserve; }

    public void AddLoopEvent(ILoopObject loopEvent)
    {
        _loopObserve.AddLoopEvent(loopEvent);
    }

    public void OnLoopEvent()
    {
        _loopObserve.OnLoopEvent();
    }
}