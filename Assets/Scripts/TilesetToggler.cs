using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesetToggler : MonoBehaviour
{
    [SerializeField] private float timeEnabled = 1f;
    [SerializeField] private float timeDisabled = 1f;
    [SerializeField] private float timeBeforeStart;
    
    private TilemapController _tilemapController;
    
    private IEnumerator Start()
    {
        _tilemapController = GetComponent<TilemapController>();
        
        yield return new WaitForSeconds(1f);
        
        while (true)
        {
            yield return new WaitForSeconds(timeBeforeStart);
            
            _tilemapController.ToggleTilemap();
            yield return new WaitForSeconds(timeEnabled);
            
            _tilemapController.ToggleTilemap();
            yield return new WaitForSeconds(timeDisabled);
        }
    }
}
