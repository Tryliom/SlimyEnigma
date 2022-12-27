using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFollower : MonoBehaviour
{
    private readonly List<GameObject> _ownedKeys = new List<GameObject>();
    
    // Update is called once per frame
    private void Update()
    {
        var keysToDestroy = new List<GameObject>();
        
        // For every key in the list, move the key to the game object's position
        foreach (var key in _ownedKeys)
        {
            if (key != null)
            {
                var position = transform.position;
                var keyTransform = key.transform;

                keyTransform.position = position;
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
