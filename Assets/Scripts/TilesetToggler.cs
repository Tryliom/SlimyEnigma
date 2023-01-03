using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TilesetToggler : MonoBehaviour
{
    [SerializeField] private float timeBeforeStart;
    [FormerlySerializedAs("timeEnabled")] [SerializeField] private float timeFirstToggled = 1f;
    [FormerlySerializedAs("timeDisabled")] [SerializeField] private float timeSecondToggled = 1f;
    [SerializeField] private bool workAlone = false;

    private TilemapController _tilemapController;
    
    private IEnumerator Start()
    {
        _tilemapController = GetComponent<TilemapController>();

        if (workAlone)
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBeforeStart);
            
                _tilemapController.ToggleTilemap();
                yield return new WaitForSeconds(timeFirstToggled);
            
                _tilemapController.ToggleTilemap();
                yield return new WaitForSeconds(timeSecondToggled);
            }
        }
    }
    
    public IEnumerator StartToggling()
    {
        yield return new WaitForSeconds(timeBeforeStart);
            
        _tilemapController.ToggleTilemap();
        yield return new WaitForSeconds(timeFirstToggled);
            
        _tilemapController.ToggleTilemap();
        yield return new WaitForSeconds(timeSecondToggled);
    }
}
