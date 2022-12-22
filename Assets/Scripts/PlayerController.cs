using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private CheckpointManager _checkpointManager;

    private Vector2 _moveValue;

    private Vector3 _lastCheckpoint;

    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int Respawning = Animator.StringToHash("Respawning");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_animator.GetBool(IsDead))
        {
            transform.Translate(_moveValue * (Time.deltaTime * _speed));
        } 
        else if (_animator.GetBool(Respawning))
        {
            // Move to the last checkpoint smoothly
            var position = transform.position;
            position = Vector3.MoveTowards(position, _lastCheckpoint, Time.deltaTime * _speed * 5f);
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
    
    public void HandleAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //_spriteRenderer.color = Color.red;
        }
        else if (context.canceled)
        {
            //_spriteRenderer.color = Color.white;
        }
        else if (context.started)
        {
            _animator.SetTrigger(Attack);
        }
    }
    
    public void Test(InputAction.CallbackContext context)
    {
        SetDead();
    }
    
    private void SetDead()
    {
        if (!_animator.GetBool(IsDead))
        {
            _animator.SetTrigger(Dead);
            _animator.SetBool(IsDead, true);
            
            // Disable the player collider
            GetComponent<Collider2D>().enabled = false;
            
            _lastCheckpoint = _checkpointManager.GetLastCheckpoint();
        }
    }
    
    public void Respawn()
    {
        if (_animator.GetBool(IsDead))
        {
            _animator.SetBool(Respawning, true);
        }
    }
    
    public void RespawnComplete()
    {
        _animator.SetBool(Respawning, false);
        _animator.SetBool(IsDead, false);
        
        GetComponent<Collider2D>().enabled = true;
        transform.rotation = Quaternion.identity;
    }
}
