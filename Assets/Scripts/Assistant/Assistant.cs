using System.Collections.Generic;
using UnityEngine;
using Utils;

/// <summary>
/// 배신자 캐릭터의 동작을 관리하는 클래스
/// FSM, 애니메이션, 루프 처리 등을 담당
/// </summary>
public class Assistant : MonoBehaviour, IMediatorEvent, ILoopObject
{
    [SerializeField] GameObject _dagger;
    [SerializeField] RectTransform _assistantUI;
    [SerializeField] DialogUI _dialogUI;

    Dictionary<EAssistantStateType, IAssistantState> _assistantState;
    IAssistantState _currentState;
    EAssistantStateType _currentType;

    Animator _animator;

    Player _player;
    MediatorManager _mediatorManager;
    MementoManager _mementoManager;
    ObserveManager _observeManager;

    Quaternion _leftDirection = Quaternion.Euler(0, 180, 0);

    #region Unity LifeCycle
    void Start()
    {
        _animator = GetComponent<Animator>();
        SetManager();
        SetMemento();
        SetState();
        OnLoopEvent();
        _dialogUI.Init();
    }

    void Update()
    {
        Loop();
    }
    #endregion

    void SetManager()
    {
        _player = GenericSingleton<PlayerManager>.Instance.Player;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _mediatorManager.Register(EMediatorEventType.PlayerLocationChanged, this);
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        _observeManager = GenericSingleton<ObserveManager>.Instance;
        _observeManager.LoopObserve.AddLoopEvent(this);
    }

    void SetMemento()
    {
        _mementoManager.AssistantMemento.AssistantScale = transform.localScale;
        _mementoManager.AssistantMemento.AssistantType = _currentType;
    }

    void SetState()
    {
        _assistantState = new Dictionary<EAssistantStateType, IAssistantState>
        {
            {EAssistantStateType.TiedUp, new TiedUpState(this) },
            {EAssistantStateType.Idle, new IdleState(this) },
            {EAssistantStateType.FollowPlayer, new FollowPlayerState(this, _player) },
            {EAssistantStateType.Kill, new KillState(this, _player) }
        };
        ChangeState(EAssistantStateType.TiedUp);
    }

    #region FSM
    /// <summary>
    /// FSM 루프 함수
    /// 현재 상태가 존재할 경우 해당 상태의 Loop 실행
    /// </summary>
    void Loop()
    {
        if (_currentState == null)
            return;
        _currentState.Loop();
    }

    /// <summary>
    /// 배신자의 현재 상태를 변경
    /// 중복 방지 및 상태 전환 시 Enter/Exit 호출
    /// </summary>
    /// <param name="newType">변경할 상태타입</param>
    public void ChangeState(EAssistantStateType newType)
    {
        if (_currentState == _assistantState[newType])
            return;
        if (_currentState != null)
            _currentState.Exit();
        _currentType = newType;
        _currentState = _assistantState[_currentType];
        _currentState.Enter();
    }
    #endregion

    /// <summary>
    /// 애니메이터의 특정 bool 파라미터 값을 변경
    /// </summary>
    /// <param name="name">파라미터의 이름</param>
    /// <param name="value">설정할 bool 값</param>
    public void ChangeAnimation(string name, bool value)
    {
        _animator.SetBool(name, value);
    }

    /// <summary>
    /// 플레이어와 배신자 간의 거리를 비교
    /// 기준 거리 이상 떨어졌는지 확인
    /// </summary>
    /// <param name="targetDistance">기준 거리</param>
    public bool CheckDistance(float targetDistance)
    {
        Vector2 temp = _player.transform.position - transform.position;
        float distance = temp.sqrMagnitude;
        if (distance > targetDistance * targetDistance)
            return true;
        return false;
    }

    public void Look()
    {
        Vector2 direction = _player.transform.position - transform.position;
        Vector3 scale = transform.localScale;

        if (direction.x > 0)
        {
            scale.x = -Mathf.Abs(scale.x);
            _assistantUI.rotation = Quaternion.Euler(Vector3.zero);
        }
        else if (direction.x < 0)
        {
            scale.x = Mathf.Abs(scale.x);
            _assistantUI.rotation = _leftDirection;
        }

        transform.localScale = scale;
    }

    #region Animation Event
    /// <summary>
    /// 플레이어 Kill 애니메이션이 끝날 때 호출
    /// </summary>
    public void KillPlayer()
    {
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.AddMemory, EMemoryType.AssistantRope);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.StartFade);
    }
    #endregion

    #region Interface
    /// <summary>
    /// 중재자 이벤트 수신 처리
    /// 플레이어 위치 변경 시, 배신자 위치도 동기화
    /// </summary>
    void IMediatorEvent.HandleEvent(object data)
    {
        if (_currentType == EAssistantStateType.TiedUp)
            return;
        Vector3 pos = (Vector3)data;
        transform.position = pos;
    }

    /// <summary>
    /// 루프 시 호출되는 함수
    /// 상태, 위치, 대거 상태 처음 상태로 복원
    /// </summary>
    public void OnLoopEvent()
    {
        ChangeState(_mementoManager.AssistantMemento.AssistantType);
        transform.position = _mementoManager.AssistantMemento.AssistantPositon;
        transform.localScale = _mementoManager.AssistantMemento.AssistantScale;
        _dagger.SetActive(false);
    }
    #endregion

    #region Unity Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EndingRoom"))
        {
            _dagger.SetActive(true);
            ChangeState(EAssistantStateType.Kill);
        }
    }
    #endregion
}