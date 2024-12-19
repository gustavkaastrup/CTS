using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PumpScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public string img_path;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private float pumpDuration = 0.2f; // Duration of the pump effect
    private float pumpTimer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = Resources.Load<Sprite>(img_path);
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        if (pumpTimer > 0)
        {
            pumpTimer -= Time.deltaTime;
            float t = 1 - (pumpTimer / pumpDuration);
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    public void Pump()
    {
        targetScale = originalScale * 1.2f;
        pumpTimer = pumpDuration;
        transform.localScale = targetScale;
    }
}
