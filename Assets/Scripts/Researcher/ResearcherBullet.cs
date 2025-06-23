using UnityEngine;
using Utils;

public class ResearcherBullet : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _lifeTime;

    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Remove", _lifeTime);
    }

    void Update()
    {
        Move();
    }

    public void Init()
    {

    }

    void Move()
    {

    }

    void Remove()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.LoopEvent);
        Remove();
    }
}