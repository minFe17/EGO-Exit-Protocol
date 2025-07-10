using UnityEngine;
using Utils;

public class TiedUpState : IAssistantState, IMediatorEvent
{
    int _enterCount;
    bool _inPlayer;

    Assistant _assistant;
    EnterPlayer _enterPlayer;
    ExitPlayer _exitPlayer;
    MediatorManager _mediatorManager;

    public TiedUpState(Assistant assistant)
    {
        _assistant = assistant;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.RopeReleased, this);
    }

    void Dialog()
    {
        Debug.Log(_enterCount);
        if (_enterCount == 1)
            _mediatorManager.Notify(EMediatorEventType.Dialog, EDialogType.AssistantFirstMeet);
        else
            _mediatorManager.Notify(EMediatorEventType.Dialog, EDialogType.AssistantRevisitRoom);
    }

    public void EnterPlayer()
    {
        _enterCount++;
        Dialog();
    }

    public void ExitPlayer()
    {
    }

    #region Interface
    void IAssistantState.Enter()
    {
        Debug.Log(1);
        if (_enterPlayer != null)
            return;
        _enterPlayer = new EnterPlayer();
        _exitPlayer = new ExitPlayer();
        _enterPlayer.Init(this);
        _exitPlayer.Init(this);
        _assistant.ChangeAnimation("isIdle", false);
    }

    void IAssistantState.Loop()
    {
      
    }

    void IAssistantState.Exit()
    {
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _assistant.ChangeState(EAssistantStateType.Idle);
    }
    #endregion
}