public class IdleState : IAssistantState
{
    Assistant _assistant;

    float _followDistance = 2f;

    public IdleState(Assistant assistant)
    {
        _assistant = assistant;
    }

    void IAssistantState.Enter()
    {
        _assistant.ChangeAnimation("isIdle", true);
    }

    void IAssistantState.Loop()
    {
        _assistant.Look();
        if (_assistant.CheckDistance(_followDistance))
            _assistant.ChangeState(EAssistantStateType.FollowPlayer);
    }

    void IAssistantState.Exit()
    {
    }
}