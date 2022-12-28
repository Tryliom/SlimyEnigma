using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        col.gameObject.GetComponent<PlayerController>().EnableBoomerang();
        Destroy(gameObject);
    }
}
