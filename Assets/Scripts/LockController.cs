using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    [SerializeField] private GameObject key;
    [SerializeField] private AudioClip unlockSound;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        // Rotate the key in the direction of the lock
        if (key != null)
        {
            var lockTransform = transform;
            var position = lockTransform.position;
            var pos = key.transform.position - position;
            var angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg + 180;
            
            // Rotate the key
            key.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == key)
        {
            _audioSource.PlayOneShot(unlockSound);
            
            Destroy(gameObject, unlockSound.length);
            Destroy(key);
        }
    }
}
