using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed = 5f;

    private Vector2 _moveValue;
    private bool _dead;
    
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_moveValue * (Time.deltaTime * _speed));
    }
    
    public void HandleMove(InputAction.CallbackContext context)
    {
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
        // Set the player dead
        if (!_animator.GetBool(IsDead))
        {
            _animator.SetTrigger(Dead);
            _animator.SetBool(IsDead, true);
        }
    }
}
