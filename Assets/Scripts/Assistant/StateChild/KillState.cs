using UnityEngine;

public class KillState : IAssistantState
{
    Assistant _assistant;
    Player _player;

    float _attackDistance = 0.35f;
    float _speed = 3f;

    public KillState(Assistant assistant, Player player)
    {
        _assistant = assistant;
        _player = player;
    }

    void Move()
    {
        if (_assistant.CheckDistance(_attackDistance))
        {
            float movePosX = _player.transform.position.x - _assistant.transform.position.x;
            Vector3 movePos = new Vector3(movePosX, 0, 0);
            _assistant.transform.Translate(movePos * _speed * Time.deltaTime);
        }
        else
            _assistant.ChangeAnimation("isAttack", true);
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

    }
}