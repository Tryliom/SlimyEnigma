using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private GameObject teleportTarget;

    private bool _canTeleport = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsCollisionValid(collision) && _canTeleport)
        {
            teleportTarget.GetComponent<Teleporter>().DisableTeleport();
            
            collision.gameObject.transform.position = teleportTarget.transform.position;

            if (collision.gameObject.CompareTag("Projectile"))
            {
                collision.gameObject.GetComponent<MaskProjectileController>().ResetLifeTime();
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
