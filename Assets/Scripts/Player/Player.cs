using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] RectTransform _ui;
    [SerializeField] GameObject _keyInfoUI;
    [SerializeField] DialogUI _dialogUI;

    Rigidbody2D _rigidbody;
    Animator _animator;
    MediatorManager _mediatorManager;
    InteractObjectManager _interactObjectManager;
    IInteractable _interactableObject;

    float _movePos;
    bool _isDialog;
    Quaternion _leftDirection = Quaternion.Euler(0, 180, 0);

    DialogEvent _dialogEvent;
    EndDialogEvent _endDialogEvent;

    #region Unity LifeCycle
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _dialogUI.Init();
        _dialogEvent = new DialogEvent(this);
        _endDialogEvent = new EndDialogEvent(this);
    }

    void FixedUpdate()
    {
        if (_isDialog)
            return;
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
        {
            transform.rotation = _leftDirection;
            _ui.localRotation = _leftDirection;
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            _ui.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void SetDialogState(bool value)
    {
        _isDialog = value;
    }

    #region Unity InputSystem
    void OnMove(InputValue value)
    {
        if (_isDialog)
            return;
        _movePos = value.Get<Vector2>().x;
        if (_movePos != 0)
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

    void OnNumber()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            GenericSingleton<PlayerManager>.Instance.UsePhone(1);
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
            GenericSingleton<PlayerManager>.Instance.UsePhone(2);
    }
    #endregion

    #region Unity Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableObject"))
        {
            _interactObjectManager.GetInteractable(out _interactableObject, collision.gameObject);
            _keyInfoUI.SetActive(true);
        }
        if (collision.gameObject.CompareTag("AssistantRoom"))
            _mediatorManager.Notify(EMediatorEventType.PlayerEnterAssistantRoom);
        if (collision.gameObject.TryGetComponent<Zone>(out Zone zone))
            zone.SetCurrentZone();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableObject"))
        {
            _interactableObject = null;
            _keyInfoUI.SetActive(false);
        }
        if (collision.gameObject.CompareTag("AssistantRoom"))
            _mediatorManager.Notify(EMediatorEventType.PlayerExitAssistantRoom);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractableObject"))
        {
            _interactObjectManager.GetInteractable(out _interactableObject, collision.gameObject);
            _keyInfoUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractableObject"))
        {
            if (_interactableObject == null)
                return;

            if (collision.gameObject == _interactableObject.GetGameObject())
            {
                _interactableObject = null;
                _keyInfoUI.SetActive(false);
            }
        }
    }
    #endregion
}