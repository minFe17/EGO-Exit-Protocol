using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Researcher : MonoBehaviour, IMediatorEvent
{
    [SerializeField] float _speed;

    List<EZoneType> _currentPath;
    EZoneType _currentZone;
    Transform _player;
    Animator _animator;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    ZoneManager _zoneManager;

    int _pathIndex = 1;

    private void Start()
    {
        _player = GenericSingleton<PlayerManager>.Instance.Player.transform;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _zoneManager = GenericSingleton<ZoneManager>.Instance;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.PlayeMoveOtherZone, this);
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Zone playerZone = _zoneManager.PlayerZone;
        if (_currentZone == playerZone.ZoneID)
            MoveToTarget(_player.position);
        else
        {
            if (_currentPath == null || _currentPath.Count == 0 || _pathIndex >= _currentPath.Count)
            {
                _currentPath = _zoneManager.FindPath(_currentZone);
                _pathIndex = 1;
            }

            if (_currentPath != null && _pathIndex < _currentPath.Count)
            {
                EZoneType nextZone = _currentPath[_pathIndex];
                ZoneLink link = _zoneManager.GetZoneLink(_currentZone, nextZone);
                if (link != null)
                {
                    MoveToTarget(link.transform.position);
                    Debug.Log(3);
                }
            }
        }
    }

    void MoveToTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        _rigidbody.linearVelocityX = direction.x * _speed;

        if (direction.x > 0)
            _spriteRenderer.flipX = false;
        else if (direction.x < 0)
            _spriteRenderer.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Zone>(out Zone zone))
            _currentZone = zone.ZoneID;
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

    void IMediatorEvent.HandleEvent(object data)
    {
        _currentPath = _zoneManager.FindPath(_currentZone);
        _pathIndex = 1;
    }
}