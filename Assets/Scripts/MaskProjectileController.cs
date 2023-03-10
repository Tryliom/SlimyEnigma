using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MaskProjectileController : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] [SerializeField] private float speed;
    [FormerlySerializedAs("_lifeTime")] [SerializeField] private float maxLifeTime;
    [SerializeField] private bool boomerang;
    [SerializeField] private List<AudioClip> maskBreakSound;

    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private Vector2 _direction;
    private bool _destroyed;
    private bool _isReturning;
    private float _lifeTime;

    private static readonly int DestroyProjectile = Animator.StringToHash("DestroyProjectile");
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        
        _lifeTime = maxLifeTime;
        _isReturning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_destroyed)
        {
            return;
        }
        
        _rb.velocity = _direction * speed;
        
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
            if (boomerang && !_isReturning)
            {
                _isReturning = true;
                _direction *= -1;
                _lifeTime = maxLifeTime;
            }
            else
            {
                StartDestroy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bulletProjectile = other.GetComponent<BulletProjectile>();
        
        if ((other.gameObject.CompareTag("Block") || bulletProjectile != null) && !_destroyed)
        {
            StartDestroy();

            if (bulletProjectile != null)
            {
                Destroy(other.gameObject);
            }
        }
    }
    
    private void StartDestroy()
    {
        _animator.SetTrigger(DestroyProjectile);
        _destroyed = true;
        _rb.velocity *= 0.2f;
        _collider.enabled = false;
        
        _audioSource.PlayOneShot(maskBreakSound[Random.Range(0, maskBreakSound.Count)]);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void ResetLifeTime()
    {
        _lifeTime = maxLifeTime;
    }
}
