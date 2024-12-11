using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpriteSwitcher : MonoBehaviour
{
    public Sprite notClickedSprite; // Default sprite
    public Sprite clickedSprite;    // Sprite after click
    public bool keepClicked;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isPressed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        if (spriteRenderer != null )
        {
            spriteRenderer.sprite = notClickedSprite;
            FitSpriteToObject();
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

    void FitSpriteToCollider()
    {
        // Get the size of the collider
        Vector2 colliderSize = boxCollider.size;

        // Get the size of the sprite in world units
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // Calculate scale factor to fit the sprite within the collider
        float scaleX = colliderSize.x / spriteSize.x;
        float scaleY = colliderSize.y / spriteSize.y;

        // Apply the scale to the sprite without affecting the collider
        transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);
    }
    private void FitSpriteToObject()
    {
        // Get the size of the sprite
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // Get the size of the object (assuming it's a square, use scale to fit the sprite)
        Vector2 objectSize = transform.localScale;

        //spriteSize.x = objectSize.x;
        //spriteSize.y = objectSize.y;

        // Calculate scale factor to make the sprite fit within the object
        float scaleFactor = Mathf.Min(objectSize.x / spriteSize.x, objectSize.y / spriteSize.y);

        // Apply the scale factor
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        boxCollider.size = objectSize;
    }
}
