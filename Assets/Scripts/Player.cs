using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;
    [SerializeField] float _speed;

    float _movePos;

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _rigidbody.linearVelocityX = _movePos * _speed;
    }

    void Turn()
    {
        if (_movePos < 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }


    // InputSystem
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
}