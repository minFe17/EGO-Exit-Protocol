using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Assistant : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rigidbody;
    Dictionary<EAssistantStateType, IAssistantState> _assistantState;
    IAssistantState _currentState;
    EAssistantStateType _currentType;

    Player _player;

    void Start()
    {
        _player = GenericSingleton<PlayerManager>.Instance.Player;
        SetState();
    }

    void Update()
    {
        Loop();
    }

    void SetState()
    {
        _assistantState = new Dictionary<EAssistantStateType, IAssistantState>
        {
            {EAssistantStateType.TiedUp, new TiedUpState(this) },
            {EAssistantStateType.Idle, new IdleState(this) },
            {EAssistantStateType.FollowPlayer, new FollowPlayerState(this, _player, _rigidbody) },
            {EAssistantStateType.Kill, new KillState() }
        };
        ChangeState(EAssistantStateType.TiedUp);
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EndingRoom"))
            ChangeState(EAssistantStateType.Kill);
    }
}