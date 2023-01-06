using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private GameObject teleportTarget;
    [SerializeField] private List<AudioClip> teleportSound;

    private AudioSource _audioSource;
    
    private bool _canTeleport = true;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsCollisionValid(collision) && _canTeleport && teleportTarget != null)
        {
            teleportTarget.GetComponent<Teleporter>().DisableTeleport();
            
            collision.gameObject.transform.position = teleportTarget.transform.position;

            if (collision.gameObject.CompareTag("Projectile"))
            {
                collision.gameObject.GetComponent<MaskProjectileController>().ResetLifeTime();
            }
            else
            {
                _audioSource.PlayOneShot(teleportSound[Random.Range(0, teleportSound.Count)]);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsCollisionValid(other))
        {
            _canTeleport = true;
        }
    }
    
    private static bool IsCollisionValid(Component collision)
    {
        return collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile");
    }

    private void DisableTeleport()
    {
        _canTeleport = false;
    }
}
