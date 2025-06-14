public class TimePause : TimeEventBase, IMediatorEvent
{
    void IMediatorEvent.HandleEvent(object data)
    {
        _timeManager.Stop();
    }
}