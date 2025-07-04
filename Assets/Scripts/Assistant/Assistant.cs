using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Assistant : MonoBehaviour, IMediatorEvent, ILoopObject
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _dagger;

    Dictionary<EAssistantStateType, IAssistantState> _assistantState;
    IAssistantState _currentState;
    EAssistantStateType _currentType;

    Player _player;
    MediatorManager _mediatorManager;
    MementoManager _mementoManager;
    ObserveManager _observeManager;

    #region Unity LifeCycle
    void Start()
    {
        SetManager();
        SetMemento();
        SetState();
        OnLoopEvent();
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
    void Loop()
    {
        if (_currentState == null)
            return;
        _currentState.Loop();
    }

    public void ChangeState(EAssistantStateType newType)
    {
        if (_currentType == newType)
            return;
        if (_currentState != null)
            _currentState.Exit();
        _currentType = newType;
        _currentState = _assistantState[_currentType];
        _currentState.Enter();
    }
    #endregion

    public void ChangeAnimation(string name, bool value)
    {
        _animator.SetBool(name, value);
    }

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
            scale.x = -Mathf.Abs(scale.x);
        else if (direction.x < 0)
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    #region Animation Event
    public void KillPlayer()
    {
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.AddMemory, EMemoryType.Assistant_Rope);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.StartFade);
    }
    #endregion

    #region Interface
    void IMediatorEvent.HandleEvent(object data)
    {
        if (_currentType == EAssistantStateType.TiedUp)
            return;
        Vector3 pos = (Vector3)data;
        transform.position = pos;
    }

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