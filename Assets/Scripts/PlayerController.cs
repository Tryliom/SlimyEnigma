using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] [SerializeField] private float speed = 5f;
    [FormerlySerializedAs("_checkpointManager")] [SerializeField] private CheckpointManager checkpointManager;
    [FormerlySerializedAs("_camera")] [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject firstAttackProjectile;
    [SerializeField] private GameObject secondAttackProjectile;
    [SerializeField] private GameObject attackDirection;

    private Vector2 _moveValue;
    private Vector3 _lastCheckpoint;

    private bool _unlockFirstAttack = false;
    private bool _unlockSecondAttack = false;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private PlayerSoundController _playerSoundController;

    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int SecondAttack = Animator.StringToHash("SecondAttack");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int Respawning = Animator.StringToHash("Respawning");

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerSoundController = GetComponent<PlayerSoundController>();
        
        attackDirection.GetComponent<AttackDirection>().HideAttackDirection();
        
        Cursor.visible = false;
        
        _playerSoundController.PlayPlayerSpawnSound();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_animator.GetBool(IsDead))
        {
            transform.Translate(_moveValue * (Time.deltaTime * speed));
        } 
        else if (_animator.GetBool(Respawning))
        {
            // Move to the last checkpoint smoothly
            var position = transform.position;
            position = Vector3.MoveTowards(position, _lastCheckpoint, Time.deltaTime * speed * 5f);
            transform.position = position;

            // Rotate the player to face the checkpoint
            var direction = _lastCheckpoint - position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            
            _spriteRenderer.flipX = false;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void FixedUpdate()
    {
        // If the player is close enough to the checkpoint, stop moving and set the player to alive
        if (_animator.GetBool(Respawning) && Vector3.Distance(transform.position, _lastCheckpoint) < 0.1f)
        {
            RespawnComplete();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Danger") && !_animator.GetBool(IsDead))
        {
            Die();

            if (col.GetComponent<BulletProjectile>())
            {
                _playerSoundController.PlayPlayerDeathBulletSound();
            }
            else
            {
                _playerSoundController.PlayPlayerDeathLavaSound();
            }
        }
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        _moveValue = Vector2.zero;
        
        if (_animator.GetBool(IsDead)) return;
        
        var input = context.ReadValue<Vector2>();

        _moveValue = input;
        
        if (input.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (input.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        
        if (input.x != 0 || input.y != 0)
        {
            _animator.SetBool(Walking, true);
        }
        else
        {
            _animator.SetBool(Walking, false);
        }
    }
    
    public void HandleFirstAttack(InputAction.CallbackContext context)
    {
        if (!_unlockFirstAttack) return;
        
        if (context.performed)
        {
            _animator.SetTrigger(Attack);
        }
    }
    
    public void HandleSecondAttack(InputAction.CallbackContext context)
    {
        if (!_unlockSecondAttack) return;
        
        if (context.performed)
        {
            _animator.SetTrigger(SecondAttack);
        }
    }
    
    private void Die()
    {
        if (!_animator.GetBool(IsDead))
        {
            _animator.SetTrigger(Dead);
            _animator.SetBool(IsDead, true);
            
            // Disable the player collider
            _boxCollider2D.enabled = false;
            
            _lastCheckpoint = checkpointManager.GetLastCheckpoint();
        }
    }
    
    public void Respawn()
    {
        if (_animator.GetBool(IsDead))
        {
            _animator.SetBool(Respawning, true);
            attackDirection.GetComponent<AttackDirection>().HideAttackDirection();
            _playerSoundController.PlayPlayerRespawningSound();
        }
    }
    
    private void RespawnComplete()
    {
        _animator.SetBool(Respawning, false);
        _animator.SetBool(IsDead, false);
        
        _boxCollider2D.enabled = true;
        transform.rotation = Quaternion.identity;
        
        if (_unlockFirstAttack)
        {
            attackDirection.GetComponent<AttackDirection>().ShowAttackDirection();
        }
        
        _playerSoundController.PlayPlayerSpawnSound();
    }

    public void UseFirstAttack()
    {
        LaunchAttack(false);
    }
    
    public void UseSecondAttack()
    {
        LaunchAttack(true);
    }
    
    private void LaunchAttack(bool secondAttack)
    {
        var position = transform.position;

        // Get mouse direction
        var attackPosition = attackDirection.transform.position;
        var direction = attackPosition - position;
        
        // If the direction is to the left, flip the sprite
        if (direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        
        // Get the position at the end of the player box collider, depend of the direction
        var colliderSize = _boxCollider2D.size;
        var colliderOffset = _boxCollider2D.offset;
        var colliderPosition = new Vector3(colliderOffset.x, colliderOffset.y, 0f);
        colliderPosition += new Vector3(colliderSize.x / 2f, colliderSize.y / 2f, 0f);
        colliderPosition *= _spriteRenderer.flipX ? -1f : 1f;
        colliderPosition += position;
        colliderPosition.z = 0f;
        
        // Create the projectile
        var projectile = Instantiate(secondAttack ? secondAttackProjectile : firstAttackProjectile, colliderPosition, Quaternion.identity);
        
        // Get the angle of the mouse direction
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Calculate the direction of the projectile from -1 to 1
        var projectileDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        projectile.transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
        projectile.GetComponent<MaskProjectileController>().SetDirection(projectileDirection);
        
        _playerSoundController.PlayPlayerAttackSound();
    }

    public void EnableAttack()
    {
        _unlockFirstAttack = true;
        
        attackDirection.GetComponent<AttackDirection>().ShowAttackDirection();
    }
    
    public void EnableBoomerang()
    {
        _unlockSecondAttack = true;
    }
}
