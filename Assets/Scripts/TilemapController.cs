using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    [SerializeField] private bool enabledByDefault;
    
    private TilemapRenderer _tilemapRenderer;
    private TilemapCollider2D _tilemapCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        _tilemapRenderer = GetComponent<TilemapRenderer>();
        
        // Try to get the TilemapCollider2D component
        _tilemapCollider2D = GetComponent<TilemapCollider2D>();
        
        if (enabledByDefault)
        {
            EnableTilemap();
        }
        else
        {
            DisableTilemap();
        }
    }

    public void EnableTilemap()
    {
        _tilemapRenderer.enabled = true;
        
        // If the TilemapCollider2D component exists, enable it
        if (_tilemapCollider2D != null)
        {
            _tilemapCollider2D.enabled = true;
        }
    }
    
    public void DisableTilemap()
    {
        _tilemapRenderer.enabled = false;
        
        // If the TilemapCollider2D component exists, disable it
        if (_tilemapCollider2D != null)
        {
            _tilemapCollider2D.enabled = false;
        }
        
    }
}
