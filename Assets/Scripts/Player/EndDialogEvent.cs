using UnityEngine;
using Utils;

public class EndDialogEvent : IMediatorEvent
{
    Player _player;

    public EndDialogEvent(Player player)
    {
        _player = player;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.EndDialog, this);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _player.SetDialogState(false);
    }
}