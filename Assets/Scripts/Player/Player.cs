using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] Animator _animator;
    [SerializeField] float _speed;

    float _movePos;
    Vector3 _leftDirection = new Vector3(-1, 1, 1);

    PlayerManager _playerManager;
    InteractObjectManager _interactObjectManager;
    IInteractable _interactableObject;

    #region Unity LifeCycle
    void Start()
    {
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _playerManager = GenericSingleton<PlayerManager>.Instance;
        _playerManager.Init(this);

        // 임시로 Player 클래스에서
        GenericSingleton<TimeManager>.Instance.Init();
        GenericSingleton<LoopManager>.Instance.Init();
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    void Move()
    {
        _rigidbody.linearVelocityX = _movePos * _speed;
    }

    void Turn()
    {
        if (_movePos < 0)
            transform.localScale = _leftDirection;
        else
            transform.localScale = Vector3.one;
    }

    #region Unity InputSystem
    void OnMove(InputValue value)
    {
        _movePos = value.Get<Vector2>().x;
        if(_movePos != 0)
        {
            Turn();
            _animator.SetBool("isMove", true);
        }
        else
            _animator.SetBool("isMove", false);
    }

    void OnInteract()
    {
        if (_interactableObject != null)
            _interactableObject.Interact();
    }
    #endregion

    #region Unity Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableObject"))
            _interactObjectManager.GetInteractable(out _interactableObject, collision.gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableObject"))
            _interactableObject = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractableObject"))
        {
            _interactObjectManager.GetInteractable(out _interactableObject, collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractableObject"))
        {
            if(_interactableObject == null)
                return;
            if(collision.gameObject == _interactableObject.GetGameObject())
                _interactableObject = null;
        }
    }
    #endregion
}