using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MechanismController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapToDisable;
    [SerializeField] private Tilemap tilemapToEnable;
    [SerializeField] private Sprite enabledSprite;
    [SerializeField] private Sprite disabledSprite;
    [SerializeField] private List<Tilemap> tilemapsToToggle;
    
    private SpriteRenderer _spriteRenderer;

    private bool _isMechanismEnabled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("Projectile")))
        {
            if (!_isMechanismEnabled)
            {
                _isMechanismEnabled = true;
                _spriteRenderer.sprite = enabledSprite;
                
                if (tilemapToDisable != null)
                {
                    tilemapToDisable.GetComponent<TilemapController>().DisableTilemap();
                }
                
                if (tilemapToEnable != null)
                {
                    tilemapToEnable.GetComponent<TilemapController>().EnableTilemap();
                }
            }
            else
            {
                _isMechanismEnabled = false;
                _spriteRenderer.sprite = disabledSprite;
                
                if (tilemapToDisable != null)
                {
                    tilemapToDisable.GetComponent<TilemapController>().EnableTilemap();
                }
                
                if (tilemapToEnable != null)
                {
                    tilemapToEnable.GetComponent<TilemapController>().DisableTilemap();
                }
            }
            
            if (tilemapsToToggle.Count > 0)
            {
                foreach (var tilemap in tilemapsToToggle)
                {
                    tilemap.GetComponent<TilemapController>().ToggleTilemap();
                }
            }
        }
    }
}
