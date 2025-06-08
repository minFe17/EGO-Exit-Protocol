using UnityEngine;

public class ObserveManager : MonoBehaviour
{
    // �̱���
    LoopObserve _loopObserve = new LoopObserve();
    DoorObserve _doorObserve = new DoorObserve();

    public DoorObserve DoorObserve { get => _doorObserve; }
    public LoopObserve LoopObserve { get => _loopObserve; }
}