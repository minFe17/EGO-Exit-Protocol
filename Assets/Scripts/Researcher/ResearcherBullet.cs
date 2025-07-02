using UnityEngine;
using Utils;

public class ResearcherBullet : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _lifeTime;

    SpriteRenderer _spriteRenderer;

    Vector3 _direction;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    public void Init(Vector3 direction)
    {
        _direction = direction;
        if (_direction.x > 0)
            _spriteRenderer.flipX = false;
        else if(_direction.x < 0)
            _spriteRenderer.flipX = true;

        Invoke("Remove", _lifeTime);
    }

    void Move()
    {
        transform.Translate(_direction * _moveSpeed * Time.deltaTime);
    }

    void Remove()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.StartFade);
        Remove();
    }
}