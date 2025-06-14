public class LoopUI : TextUI, IMediatorEvent
{
    protected override void SetMediatorEvent()
    {
        _mediatorManager.Register(EMediatorEventType.ChangeLoopCount, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _text.text = data.ToString();
    }
}