using UnityEngine;
using Utils;

public class MoveState : MonoBehaviour, IResearcherState
{
    Researcher _researcher;
    ZoneManager _zoneManager;
    Transform _player;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;

    float _speed = 3.5f;
    float _attackDistance = 4f;

    public MoveState(Researcher researcher, Rigidbody2D rigidbody, SpriteRenderer spriteRenderer)
    {
        _researcher = researcher;
        _rigidbody = rigidbody;
        _spriteRenderer = spriteRenderer;

        if (_zoneManager == null)
            _zoneManager = GenericSingleton<ZoneManager>.Instance;
        if (_player == null)
            _player = GenericSingleton<PlayerManager>.Instance.Player.transform;
    }

    void Move()
    {
        Zone playerZone = _zoneManager.PlayerZone;
        if (_researcher.CurrentZone == playerZone.ZoneID)
            MoveToTarget(_player.position);
        else
        {
            if (_researcher.CurrentPath == null || _researcher.CurrentPath.Count == 0 || _researcher.PathIndex >= _researcher.CurrentPath.Count)
                _researcher.HandleEvent(null);

            if (_researcher.CurrentPath != null && _researcher.PathIndex < _researcher.CurrentPath.Count)
            {
                EZoneType nextZone = _researcher.CurrentPath[_researcher.PathIndex];
                ZoneLink link = _zoneManager.GetZoneLink(_researcher.CurrentZone, nextZone);
                if (link != null)
                {
                    MoveToTarget(link.transform.position);
                }
            }
        }
    }

    void MoveToTarget(Vector3 target)
    {
        Vector3 direction = (target - _researcher.transform.position).normalized;
        _rigidbody.linearVelocityX = direction.x * _speed;

        if (direction.x > 0)
            _spriteRenderer.flipX = false;
        else if (direction.x < 0)
            _spriteRenderer.flipX = true;
    }

    void IResearcherState.Enter()
    {
        _researcher.ChangeAnimation("isMove", true);
    }

    void IResearcherState.Loop()
    {
        Move();
        if(_researcher.CheckAttackArea(_attackDistance))
            _researcher.ChangeState(EResearcherStateType.Attack);
    }

    void IResearcherState.Exit()
    {
    }
}