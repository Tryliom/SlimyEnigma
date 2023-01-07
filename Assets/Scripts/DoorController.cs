using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject padlock;
    [SerializeField] private Sprite openDoor;
    [SerializeField] private AudioClip doorSound;
    
    [SerializeField] private GameObject chronoController;
    [SerializeField] private bool stopChrono;

    private bool _isOpen = false;
    
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        CheckDoor();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        CheckDoor();
    }
    
    private void CheckDoor()
    {
        if (padlock == null && !_isOpen)
        {
            _spriteRenderer.sprite = openDoor;
            _boxCollider2D.enabled = false;
            _isOpen = true;
            
            _audioSource.PlayOneShot(doorSound);
            
            if (stopChrono)
            {
                chronoController.GetComponent<ChronoController>().StopChrono();
            }
        }
    }
}
