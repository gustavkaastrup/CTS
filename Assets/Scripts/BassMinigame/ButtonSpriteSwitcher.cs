using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpriteSwitcher : MonoBehaviour
{
    public Sprite notClickedSprite; // Default sprite
    public Sprite clickedSprite;    // Sprite after click
    public bool keepClicked;

    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null )
        {
            spriteRenderer.sprite = notClickedSprite;
        }
    }

    private void OnMouseDown()
    {
        if (spriteRenderer != null)
        {
            if (keepClicked)
            {
                isPressed = !isPressed;
                spriteRenderer.sprite = isPressed ? clickedSprite : notClickedSprite;
            }
            else
            {
                spriteRenderer.sprite = clickedSprite;
            }
        }
    }
    private void OnMouseUp()
    {
        if (spriteRenderer != null)
        {
            if (!keepClicked)
            {
                spriteRenderer.sprite = notClickedSprite;

            }
        }
    }
}
