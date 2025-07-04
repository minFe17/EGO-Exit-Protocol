using UnityEngine;
using Utils;

public class TimeManager : MonoBehaviour, IMediatorEvent, ILoopObject
{
    // �̱���
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
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.EndFade, this);
        SetManager();
        _observeManager.LoopObserve.AddLoopEvent(this);
        _timePause.Init();
        _timeResume.Init();

        _timer = _mementoManager.TimeMemento.LoopTime;
        _isStop = _mementoManager.TimeMemento.IsStop;
        _isLoop = _mementoManager.TimeMemento.IsLoop;
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
    public void HandleEvent(object data = null)
    {
        Resume();
        _isLoop = false;
    }

    void ILoopObject.OnLoopEvent()
    {
        _timer = _mementoManager.TimeMemento.LoopTime;
        _isStop = _mementoManager.TimeMemento.IsStop;
        _isLoop = _mementoManager.TimeMemento.IsLoop;
        _previousTime = (int)_timer;
        _mediatorManager.Notify(EMediatorEventType.TimeTick, (int)_timer);
    }
    #endregion
}