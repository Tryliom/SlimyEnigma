using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManipulator : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private readonly List<GameObject> _ownedKeys = new List<GameObject>();
    
    // Update is called once per frame
    private void Update()
    {
        var keysToDestroy = new List<GameObject>();
        
        // For every key in the list, move the key at the direction of the mouse
        foreach (var key in _ownedKeys)
        {
            if (key != null)
            {
                var mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                var position = transform.position;
                var direction = mousePosition - position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                var keyTransform = key.transform;
            
                // Add offset based on the angle of the mouse
                var offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

                keyTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                keyTransform.position = position + direction.normalized * 0.5f + offset;
            }
            else
            {
                keysToDestroy.Add(key);
            }
        }
        
        // Remove all the keys that are null
        foreach (var key in keysToDestroy)
        {
            _ownedKeys.Remove(key);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Key"))
        {
            _ownedKeys.Add(col.gameObject);
        }
    }
}
