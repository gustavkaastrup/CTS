using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PPText3 : MonoBehaviour
{
    public string hit;  
    private Text textComponent; 

    void Start()
    {
        // Get the Text component
        textComponent = GetComponent<Text>(); 

    }

    void Update()
    {
        // Get the hit type from PlayerPrefs
        string hitType = PlayerPrefs.GetString(hit);

        // If hitType is empty or null, set text to empty
        if (string.IsNullOrEmpty(hitType))
        {
            textComponent.text = "";
            textComponent.color = Color.gray; // Set a default color if needed (gray in this case)
            return; // Exit the Update method early
        }

        // Update the text with the current hit type
        textComponent.text = hitType;

        // Change color based on the hit type
        switch (hitType)
        {
            case "Perfect":
                textComponent.color = Color.green; // Perfect hits are green
                break;
            case "Great":
                textComponent.color = Color.blue; // Great hits are blue
                break;
            case "Good":
                textComponent.color = Color.yellow; // Good hits are yellow
                break;
            case "Barely Hit":
                textComponent.color = Color.red; // Barely hits are red
                break;
            default:
                textComponent.color = Color.gray; // Unknown hits or misses are gray
                break;
        }
    }
}