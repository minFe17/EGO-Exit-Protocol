using UnityEngine;
using Utils;

public class LoopManager : MonoBehaviour, ILoopObject
{
    // ╫л╠шео
    int _loopCount;

    public void Init()
    {
        GenericSingleton<ObserveManager>.Instance.AddLoopEvent(this);
    }

    void ILoopObject.OnLoopEvent()
    {
        _loopCount++;
    }
}