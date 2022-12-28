using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTextOnDestroy : MonoBehaviour
{
    [SerializeField] private string textToDisplay;
    [SerializeField] private float displayTime;
    [SerializeField] private GameObject textPrefab;
    
    private void OnDestroy()
    {
        // Display text on the transform position
        var text = Instantiate(textPrefab, transform.position, Quaternion.identity);
        text.GetComponent<TextMeshPro>().text = textToDisplay;
        Destroy(text, displayTime);
    }
}
