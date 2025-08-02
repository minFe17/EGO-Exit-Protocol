using UnityEngine;
using Utils;

/// <summary>
/// 연구원(적)의 이동 상태 클래스
/// FSM 패턴으로 구현
/// Zone 기반 경로 탐색을 통해 플레이어 추적
/// 일정 거리 내에 플레이어가 감지되면 공격 상태로 변경
/// </summary>
public class MoveState : IResearcherState
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

    /// <summary>
    /// 현재 경로 또는 플레이어 위치를 기준으로 이동시킴
    /// 경로가 유효하지 않으면 이벤트 핸들링을 통해 경로를 다시 요청
    /// </summary>
    void Move()
    {
        Zone playerZone = _zoneManager.PlayerZone;

        // 플레이어와 같은 존에 있으면 플레이어에게 이동
        if (_researcher.CurrentZone == playerZone.ZoneID)
        {
            MoveToTarget(_player.position);
            return;
        }

        // 현재 경로의 유효성 검사
        bool noPath = _researcher.CurrentPath == null;
        bool emptyPath = !noPath && _researcher.CurrentPath.Count == 0;
        bool indexInvalid = !noPath && _researcher.PathIndex >= _researcher.CurrentPath.Count;

        if (noPath || emptyPath || indexInvalid)
        {
            // 경로 재탐색
            _researcher.HandleEvent(null);
            return;
        }

        // 다음 Zone으로 가기 위한 링크 위치 계산
        EZoneType nextZone = _researcher.CurrentPath[_researcher.PathIndex];
        ZoneLink link = _zoneManager.GetZoneLink(_researcher.CurrentZone, nextZone);
        if (link == null)
            return;

        MoveToTarget(link.transform.position);
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

    // 상태 진입 시 호출
    void IResearcherState.Enter()
    {
        _researcher.ChangeAnimation("isMove", true);
    }

    void IResearcherState.Loop()
    {
        Move();
        if (_researcher.CheckAttackArea(_attackDistance))
            _researcher.ChangeState(EResearcherStateType.Attack);
    }

    // 현재 이동 상태 종료 시 별도 처리가 필요 없어 비워둠
    void IResearcherState.Exit() { }
}