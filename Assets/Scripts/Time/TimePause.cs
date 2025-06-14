public class TimePause : TimeEventBase, IMediatorEvent
{
    public override void Init()
    {
        base.Init();
        _mediatorManager.Register(EMediatorEventType.TimePause, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _timeManager.Stop();
    }
}