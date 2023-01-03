using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction;
    }
}
