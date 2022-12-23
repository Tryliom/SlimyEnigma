using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskProjectileController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _direction;
    private bool _destroyed;

    private static readonly int DestroyProjectile = Animator.StringToHash("DestroyProjectile");
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_destroyed)
        {
            return;
        }
        
        _rb.velocity = _direction * _speed;
        
        if (_direction.x > 0)
        {
            _spriteRenderer.flipY = true;
        }
        else
        {
            _spriteRenderer.flipY = false;
        }

        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            StartDestroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block") && !_destroyed)
        {
            StartDestroy();
        }
    }
    
    private void StartDestroy()
    {
        _animator.SetTrigger(DestroyProjectile);
        _destroyed = true;
        _rb.velocity = Vector2.zero;
        _collider.enabled = false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}
