public class TimeUI : TextUI, IMediatorEvent
{
    protected override void SetMediatorEvent()
    {
        _mediatorManager.Register(EMediatorEventType.TimeTick, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        int second = (int)data;
        _text.text = string.Format("{0:D2}:{1:D2}", second / 60, second % 60);
    }
}