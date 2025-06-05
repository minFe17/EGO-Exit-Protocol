using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] Animator _animator;
    [SerializeField] float _speed;

    float _movePos;
    Vector3 _leftDirection = new Vector3(-1, 1, 1);

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
            transform.localScale = _leftDirection;
        else
            transform.localScale = Vector3.one;
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