using UnityEngine;
using Utils;

public class LoopManager : MonoBehaviour, ILoopObject
{
    // �̱���
    int _loopCount;

    public void Init()
    {
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
    }

    void ILoopObject.OnLoopEvent()
    {
        _loopCount++;
    }
}