using Utils;

public class DialogEvent : IMediatorEvent
{
    Player _player;

    public DialogEvent(Player player)
    {
        _player = player;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.Dialog, this);
    }

    #region Interface
    void IMediatorEvent.HandleEvent(object data)
    {
        _player.SetDialogState(true);
    }
    #endregion
}