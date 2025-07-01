using UnityEngine;
using Utils;

public class TimeManager : MonoBehaviour, ILoopObject
{
    // 싱글턴
    MementoManager _mementoManager;
    MediatorManager _mediatorManager;
    ObserveManager _observeManager;

    TimePause _timePause = new TimePause();
    TimeResume _timeResume = new TimeResume();

    int _previousTime;
    float _timer;
    bool _isStop;
    bool _isLoop;

    public void Init()
    {
        SetManager();
        _observeManager.LoopObserve.AddLoopEvent(this);
        OnLoopEvent();
        _timePause.Init();
        _timeResume.Init();
    }

    void Update()
    {
        UpdateTimer();
    }

    void SetManager()
    {
        if (_mementoManager == null)
            _mementoManager = GenericSingleton<MementoManager>.Instance;
        if (_mediatorManager == null)
            _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        if (_observeManager == null)
            _observeManager = GenericSingleton<ObserveManager>.Instance;
    }

    void UpdateTimer()
    {
        if (_isStop || _isLoop)
            return;

        _timer -= Time.deltaTime;
        int currentTime = Mathf.Max(0, (int)_timer);

        if (currentTime != _previousTime)
        {
            _previousTime = currentTime;
            _mediatorManager.Notify(EMediatorEventType.TimeTick, currentTime);
        }

        if (_timer <= 0)
        {
            _mediatorManager.Notify(EMediatorEventType.SpawnResearcher);
            _isLoop = true;
        }
    }

    public void Stop()
    {
        if (_isStop)
            return;
        _isStop = true;
    } 

    public void Resume()
    {
        if (!_isStop)
            return;
        _isStop = false;
    }

    #region Interface
    public void OnLoopEvent()
    {
        _timer = _mementoManager.TimeMemento.LoopTime;
        // fade 효과 제작 후 주석 해제
        //_isStop = _mementoManager.TimeMemento.IsStop;
        //_isLoop = _mementoManager.TimeMemento.IsLoop;
        _previousTime = (int)_timer;
    }
    #endregion
}