using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MechanismController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapToDisable;
    [SerializeField] private Tilemap tilemapToEnable;
    [SerializeField] private Sprite enabledSprite;
    [SerializeField] private Sprite disabledSprite;
    [SerializeField] private Sprite enabledSpriteOneTime;
    [SerializeField] private Sprite disabledSpriteOneTime;
    [SerializeField] private bool oneTimeUse = false;
    [SerializeField] private List<Tilemap> tilemapsToToggle;
    [SerializeField] private AudioClip activatorSound;

    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private bool _isMechanismEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        if (oneTimeUse)
        {
            _spriteRenderer.sprite = disabledSpriteOneTime;
        }
        else
        {
            _spriteRenderer.sprite = disabledSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (oneTimeUse && _isMechanismEnabled)
        {
            return;
        }
        
        if ((other.CompareTag("Player") || other.CompareTag("Projectile")))
        {
            if (!_isMechanismEnabled)
            {
                _isMechanismEnabled = true;
                
                _spriteRenderer.sprite = oneTimeUse ? enabledSpriteOneTime : enabledSprite;
                
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
                _spriteRenderer.sprite = oneTimeUse ? disabledSpriteOneTime : disabledSprite;
                
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
        
        _audioSource.PlayOneShot(activatorSound);
    }
}
