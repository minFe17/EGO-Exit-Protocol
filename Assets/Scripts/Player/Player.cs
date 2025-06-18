using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] Animator _animator;
    [SerializeField] float _speed;
    [SerializeField] RectTransform _keyInfoUI;

    float _movePos;
    Quaternion _leftDirection = Quaternion.Euler(0, 180, 0);

    MediatorManager _mediatorManager;
    InteractObjectManager _interactObjectManager;
    IInteractable _interactableObject;

    #region Unity LifeCycle
    void Start()
    {
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
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
        {
            transform.rotation = _leftDirection;
            _keyInfoUI.localRotation = _leftDirection;
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            _keyInfoUI.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    #region Unity InputSystem
    void OnMove(InputValue value)
    {
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
    #endregion

    #region Unity Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableObject"))
        {
            _interactObjectManager.GetInteractable(out _interactableObject, collision.gameObject);
            _keyInfoUI.gameObject.SetActive(true);
        }
        if (collision.gameObject.CompareTag("AssistantRoom"))
            _mediatorManager.Notify(EMediatorEventType.PlayerEnterAssistantRoom);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractableObject"))
        {
            _interactableObject = null;
            _keyInfoUI.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("AssistantRoom"))
            _mediatorManager.Notify(EMediatorEventType.PlayerExitAssistantRoom);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractableObject"))
        {
            _interactObjectManager.GetInteractable(out _interactableObject, collision.gameObject);
            _keyInfoUI.gameObject.SetActive(true);
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
                _keyInfoUI.gameObject.SetActive(false);
            }
        }
    }
    #endregion
}