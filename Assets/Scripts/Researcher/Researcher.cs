using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Researcher : MonoBehaviour, IMediatorEvent, ILoopObject
{
    [SerializeField] Transform _bulletPos;

    Dictionary<EResearcherStateType, IResearcherState> _researcherStateDict;
    IResearcherState _currentState;
    EResearcherStateType _currentType;

    Animator _animator;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    ZoneManager _zoneManager;
    Transform _player;
    List<EZoneType> _currentPath;
    EZoneType _currentZone;

    int _pathIndex = 1;

    public List<EZoneType> CurrentPath { get => _currentPath; }
    public EZoneType CurrentZone { get => _currentZone; }
    public int PathIndex { get => _pathIndex; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _zoneManager = GenericSingleton<ZoneManager>.Instance;
        _player = GenericSingleton<PlayerManager>.Instance.Player.transform;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.PlayeMoveOtherZone, this);
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        SetState();
    }

    private void Update()
    {
        Loop();
    }

    void SetState()
    {
        _researcherStateDict = new Dictionary<EResearcherStateType, IResearcherState>
        {
            {EResearcherStateType.Move, new MoveState(this, _rigidbody, _spriteRenderer) },
            {EResearcherStateType.Attack, new AttackState(this) }
        };
        ChangeState(EResearcherStateType.Move);
    }

    void Loop()
    {
        if (_currentState == null)
            return;
        _currentState.Loop();
    }

    public void ChangeState(EResearcherStateType type)
    {
        if (_currentState == _researcherStateDict[type])
            return;
        if (_currentState != null)
            _currentState.Exit();
        _currentType = type;
        _currentState = _researcherStateDict[_currentType];
        _currentState.Enter();
    }

    public void ChangeAnimation(string name, bool value)
    {
        _animator.SetBool(name, value);
    }

    public bool CheckAttackArea(float targetDistance)
    {
        float distance = (transform.position - _player.position).sqrMagnitude;
        return distance <= targetDistance * targetDistance;
    }

    #region Animation Event
    public void MakeBullet()
    {
        GenericSingleton<ResearcherManager>.Instance.MakeBullet(_bulletPos, _player.position);
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Zone>(out Zone zone))
            _currentZone = zone.ZoneID;
        if (collision.gameObject.CompareTag("Player"))
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.StartFade);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ZoneLink>(out ZoneLink link))
        {
            if (link.FromZone == _currentZone && _currentPath != null && _pathIndex < _currentPath.Count)
            {
                if (link.ToZone == _currentPath[_pathIndex])
                {
                    transform.position = link.TargetPosition.position;
                    _currentZone = link.ToZone;
                    _pathIndex++;
                }
            }
        }
    }

    public void HandleEvent(object data)
    {
        _currentPath = _zoneManager.FindPath(_currentZone);
        _pathIndex = 1;
    }

    void ILoopObject.OnLoopEvent()
    {
        GenericSingleton<ObserveManager>.Instance.LoopObserve.RemoveLoopEvent(this);
        Destroy(this.gameObject);
    }
}