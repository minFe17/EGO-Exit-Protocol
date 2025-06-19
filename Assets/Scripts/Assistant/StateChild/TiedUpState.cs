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

    public void EnterPlayer()
    {
        _enterCount++;
        _inPlayer = true;
    }

    public void ExitPlayer()
    {
        _inPlayer = false;
    }

    void IAssistantState.Enter()
    {
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
        if(_inPlayer)
        {
            // ��� ó��
            // �� Ǯ���־����� üũ?
            // enterCount�� ���� ��� �ٸ���
        }
    }

    void IAssistantState.Exit()
    {

    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _assistant.ChangeState(EAssistantStateType.Idle);
    }
}