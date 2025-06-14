public class TimeResume : TimeEventBase, IMediatorEvent
{
    void IMediatorEvent.HandleEvent(object data)
    {
        _timeManager.Resume();
    }
}