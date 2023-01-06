using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManipulator : MonoBehaviour
{
    [SerializeField] private GameObject attackDirection;

    private readonly List<GameObject> _ownedKeys = new List<GameObject>();
    
    private AttackDirection _attackDirection;
    private PlayerController _playerController;
    private PlayerSoundController _playerSoundController;

    private void Start()
    {
        _attackDirection = attackDirection.GetComponent<AttackDirection>();
        _playerController = GetComponent<PlayerController>();
        _playerSoundController = GetComponent<PlayerSoundController>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        var keysToDestroy = new List<GameObject>();
        
        // For every key in the list, move the key at the direction of the mouse
        foreach (var key in _ownedKeys)
        {
            if (key != null)
            {
                // Get the angle offset based on how many keys are in the list
                var angle = _attackDirection.GetAngle();
                var keyTransform = key.transform;

                // Add offset based on the angle of the mouse
                var offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
                
                keyTransform.position = _playerController.transform.position + offset * (1f + _ownedKeys.IndexOf(key) * 0.3f);
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
        if (col.CompareTag("Key") && !_ownedKeys.Contains(col.gameObject))
        {
            _ownedKeys.Add(col.gameObject);
            
            _playerSoundController.PlayKeySound();
        }
    }
}
