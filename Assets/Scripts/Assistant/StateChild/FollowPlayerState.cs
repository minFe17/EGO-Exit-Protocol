using UnityEngine;

public class FollowPlayerState : IAssistantState
{
    Rigidbody2D _rigidbody;
    Assistant _assistant;
    Player _player;

    float _stopDistance = 1.5f;
    float _speed = 2f;

    public FollowPlayerState(Assistant assistant, Player player, Rigidbody2D rigidbody)
    {
        _assistant = assistant;
        _player = player;
        _rigidbody = rigidbody;
    }

    void Move()
    {
        if(_assistant.CheckDistance(_stopDistance))
        {
            float movePos = _player.transform.position.x - _assistant.transform.position.x;
            _rigidbody.linearVelocityX = movePos * _speed;
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
