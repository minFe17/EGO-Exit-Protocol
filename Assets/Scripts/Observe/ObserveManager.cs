using UnityEngine;

public class ObserveManager : MonoBehaviour
{
    // �̱���
    LoopObserve _loopObserve = new LoopObserve();

    public LoopObserve LoopObserve { get => _loopObserve; }
}