using UnityEngine;

public class ObserveManager : MonoBehaviour
{
    // ╫л╠шео
    LoopObserve _loopObserve = new LoopObserve();
    DoorObserve _doorObserve = new DoorObserve();

    public DoorObserve DoorObserve { get => _doorObserve; }
    public LoopObserve LoopObserve { get => _loopObserve; }
}