using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackDirection : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float radius = 5f;
    
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    private float _angle;

    // Start is called before the first frame update
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void HandleAttackDirectionByMouse(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        var input = context.ReadValue<Vector2>();

        if (Camera.main != null)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(input);
            var position = center.position;
            var direction = mousePosition - position;
            _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the position relative to the angle and the radius from the center
            _transform.position = position + Quaternion.Euler(0, 0, _angle) * Vector3.right * radius;
        }
    }
    
    public void HandleAttackDirectionByJoystick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        var input = context.ReadValue<Vector2>();
        var position = center.position;
        _angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        
        // Set the position relative to the angle and the radius from the center
        _transform.position = position + Quaternion.Euler(0, 0, _angle) * Vector3.right * radius;
    }
    
    public void HideAttackDirection()
    {
        _spriteRenderer.enabled = false;
    }
    
    public void ShowAttackDirection()
    {
        _spriteRenderer.enabled = true;
    }
    
    public float GetAngle()
    {
        return _angle;
    }
}
