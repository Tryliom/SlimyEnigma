using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    [SerializeField] private GameObject key;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == key)
        {
            Destroy(gameObject);
            Destroy(key);
        }
    }
}
