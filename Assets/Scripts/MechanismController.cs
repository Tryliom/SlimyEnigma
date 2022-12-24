using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MechanismController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapToEnable;
    [SerializeField] private Tilemap tilemapToDisable;
    [SerializeField] private Sprite enabledSprite;
    
    private SpriteRenderer _spriteRenderer;

    private bool _isMechanismEnabled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Projectile")) && !_isMechanismEnabled)
        {
            tilemapToEnable.GetComponent<TilemapController>().EnableTilemap();
            tilemapToDisable.GetComponent<TilemapController>().DisableTilemap();
            
            _spriteRenderer.sprite = enabledSprite;
            _isMechanismEnabled = true;
        }
    }
}
