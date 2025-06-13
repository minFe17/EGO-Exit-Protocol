using UnityEngine;
using Utils;

public class TimeManager : MonoBehaviour, ILoopObject
{
    // ╫л╠шео
    ObserveManager _observeManager;
    float _loopTime = 30f;
    float _timer;
    bool _isStop;

    public void Init()
    {
        if (_observeManager == null)
            _observeManager = GenericSingleton<ObserveManager>.Instance;
        _observeManager.LoopObserve.AddLoopEvent(this);
        OnLoopEvent();
    }

    void Update()
    {
        if (_isStop)
            return;
        _timer -= Time.deltaTime;
        if (_timer <= 0)
            _observeManager.LoopObserve.OnLoopEvent();
    }

    public void Stop()
    {
        _isStop = true;
    }

    public void Resume()
    {
        _isStop = false;
    }

    public void OnLoopEvent()
    {
        //Stop();
        _timer = _loopTime;
    }
}