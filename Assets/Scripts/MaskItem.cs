using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        col.gameObject.GetComponent<PlayerController>().EnableAttack();
        //TODO: Show message to player
        Destroy(gameObject);
    }
}
