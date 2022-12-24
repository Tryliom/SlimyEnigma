using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject padlock;
    [SerializeField] private Sprite openDoor;
    
    private bool _isOpen = false;
    
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (padlock == null && !_isOpen)
        {
            _spriteRenderer.sprite = openDoor;
            _boxCollider2D.enabled = false;
            _isOpen = true;
        }
    }
}
