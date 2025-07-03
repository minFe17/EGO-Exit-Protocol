using UnityEngine;

public class FollowPlayerState : IAssistantState
{
    Assistant _assistant;
    Player _player;

    float _stopDistance = 1.5f;
    float _speed = 2f;

    public FollowPlayerState(Assistant assistant, Player player)
    {
        _assistant = assistant;
        _player = player;
    }

    void Move()
    {
        if(_assistant.CheckDistance(_stopDistance))
        {
            float movePosX = _player.transform.position.x - _assistant.transform.position.x;
            Vector3 movePos = new Vector3(movePosX, 0, 0);
            _assistant.transform.Translate(movePos * _speed * Time.deltaTime);
        }
        else
            _assistant.ChangeState(EAssistantStateType.Idle);
    }

    void IAssistantState.Enter()
    {
        _assistant.ChangeAnimation("isWalk", true);
    }

    void IAssistantState.Loop()
    {
        Move();
    }

    void IAssistantState.Exit()
    {
        _assistant.ChangeAnimation("isWalk", false);
    }
}
