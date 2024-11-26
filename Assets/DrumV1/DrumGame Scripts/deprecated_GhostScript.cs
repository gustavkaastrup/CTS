using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class deprecated_GhostScript : MonoBehaviour
{
    private Renderer rend;
    public float blinkInterval = 0.1f; // Duration in seconds

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(Blink());
    }
    IEnumerator Blink()
    {
        while (true)
        {
            rend.enabled = !rend.enabled; // Toggle renderer visibility
            yield return new WaitForSecondsRealtime(blinkInterval); // Wait for the specified interval
        }
    }
}
