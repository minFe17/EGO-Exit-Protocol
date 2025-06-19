public class LoopUI : TextUI, IMediatorEvent
{
    protected override void SetMediatorEvent()
    {
        _mediatorManager.Register(EMediatorEventType.ChangeLoopCount, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        int loopCount = (int)data;
        _text.text = loopCount.ToString();
    }
}