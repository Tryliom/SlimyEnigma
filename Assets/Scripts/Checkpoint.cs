using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Sprite _disabledSprite;

    private CheckpointManager _checkpointManager;
    private SpriteRenderer _spriteRenderer;
    
    private bool _isCheckpointActive;
    
    // Start is called before the first frame update
    void Start()
    {
        _checkpointManager = GetComponentInParent<CheckpointManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isCheckpointActive = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isCheckpointActive)
        {
            _checkpointManager.SetLastCheckpoint(transform);
        }
    }

    public void SetCheckpointActive(bool isActive)
    {
        _isCheckpointActive = isActive;
        
        if (isActive)
        {
            _spriteRenderer.sprite = _enabledSprite;
        }
        else
        {
            _spriteRenderer.sprite = _disabledSprite;
        }
    }
}
