using UnityEngine;
using Utils;

public class TimeManager : MonoBehaviour, ILoopObject
{
    // ╫л╠шео
    ObserveManager _observeManager;
    float _loopTime = 30f;
    float _timer;

    public void Init()
    {
        if (_observeManager == null)
            _observeManager = GenericSingleton<ObserveManager>.Instance;
        _observeManager.AddLoopEvent(this);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _loopTime)
            _observeManager.OnLoopEvent();
    }

    void ILoopObject.OnLoopEvent()
    {
        _timer = 0;
    }
}