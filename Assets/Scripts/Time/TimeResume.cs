public class TimeResume : TimeEventBase, IMediatorEvent
{
    public override void Init()
    {
        base.Init();
        _mediatorManager.Register(EMediatorEventType.TimeResume, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _timeManager.Resume();
    }
}