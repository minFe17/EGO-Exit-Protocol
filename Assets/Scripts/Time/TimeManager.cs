using UnityEngine;
using Utils;

public class TimeManager : MonoBehaviour, ILoopObject
{
    // �̱���
    ObserveManager _observeManager;
    float _loopTime = 30f;
    float _timer;
    bool _isStop;

    public void Init()
    {
        if (_observeManager == null)
            _observeManager = GenericSingleton<ObserveManager>.Instance;
        _observeManager.AddLoopEvent(this);
    }

    void Update()
    {
        if (_isStop)
            return;
        _timer += Time.deltaTime;
        if (_timer > _loopTime)
            _observeManager.OnLoopEvent();
    }

    public void Stop()
    {
        _isStop = true;
    }

    public void Resume()
    {
        _isStop = false;
    }

    void ILoopObject.OnLoopEvent()
    {
        //Stop();
        _timer = 0;
    }
}