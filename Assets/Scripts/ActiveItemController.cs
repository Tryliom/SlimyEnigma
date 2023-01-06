using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveItemController : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private string text;
    [SerializeField] private float timeDisplayed = 2f;
    [SerializeField] private bool enableFirstAttack = false;
    [SerializeField] private bool enableSecondAttack = false;
    [SerializeField] private AudioClip pickItemSound;
    
    private Animator _animator;
    private AudioSource _audioSource;
    
    private bool _destroyed = false;
    
    private static readonly int PickedUp = Animator.StringToHash("PickedUp");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_destroyed)
        {
            transform.Translate(0, 1f * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player") || _destroyed) return;
        
        if (enableFirstAttack)
        {
            col.gameObject.GetComponent<PlayerController>().EnableAttack();
        }
        
        if (enableSecondAttack)
        {
            col.gameObject.GetComponent<PlayerController>().EnableBoomerang();
        }
        
        StartDestroy();
    }

    private void StartDestroy()
    {
        _animator.SetTrigger(PickedUp);
        _destroyed = true;
        _audioSource.PlayOneShot(pickItemSound);
    }

    public void Destroy()
    {
        // Display text
        var textObject = Instantiate(textPrefab, transform.position, Quaternion.identity);
        textObject.GetComponent<TextMeshPro>().text = text;
        
        Destroy(textObject, timeDisplayed);
        Destroy(gameObject, timeDisplayed + 0.1f);
    }
}
