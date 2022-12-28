using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesetToggler : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapToToggle;
    [SerializeField] private float timeBetweenToggles = 1f;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenToggles);
            tilemapToToggle.GetComponent<TilemapController>().ToggleTilemap();
        }
    }
}
