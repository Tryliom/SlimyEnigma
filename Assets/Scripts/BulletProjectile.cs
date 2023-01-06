using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private List<AudioClip> cannonBulletBreakSound;
    
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Player"))
        {
            var sound = cannonBulletBreakSound[Random.Range(0, cannonBulletBreakSound.Count)];
            _audioSource.PlayOneShot(sound);
            
            Destroy(gameObject, sound.length + 0.1f);
            
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction;
    }
}
