using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TogglerSynchronizer : MonoBehaviour
{
    [SerializeField] private float totalTimeEachToggle = 1f;
    [SerializeField] private GameObject[] togglerObjects;

    private TilesetToggler[] _togglerScripts;
    
    private void Start()
    {
        _togglerScripts = new TilesetToggler[togglerObjects.Length];
        for (int i = 0; i < togglerObjects.Length; i++)
        {
            _togglerScripts[i] = togglerObjects[i].GetComponent<TilesetToggler>();
        }
        
        StartCoroutine(Toggle());
    }

    private IEnumerator Toggle()
    {
        yield return new WaitForSeconds(1f);
        
        while (true)
        {
            for (var i = 0; i < _togglerScripts.Length; i++)
            {
                StartCoroutine(_togglerScripts[i].StartToggling());
            }
            
            yield return new WaitForSeconds(totalTimeEachToggle);
        }
    }
}
