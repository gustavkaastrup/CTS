using UnityEngine;

public class UnlockableScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    SpriteRenderer padlockSpriteRenderer;
    private string[] lock_img;
    public bool isLocked = true;
    public int levelIndex;
    private Vector3 originalScale;
    private Color originalColor;
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        originalColor = spriteRenderer.color;
        if(isLocked){
            Lock();
        }else{ 
            Unlock();
        }
        if(Loader.Instance.GetLevelIndex() >= levelIndex){
            Unlock();
        } else {
            Lock();
        }
    }
    public void Unlock(){
        isLocked = false;
        spriteRenderer.color = Color.white;
    }
    public void Lock(){
        isLocked = true;
        spriteRenderer.color = Color.black;

    }
    void OnMouseEnter()
    {
        // Enlarge the sprite and change its color when the mouse enters
        if(!isLocked){
            transform.localScale = originalScale * 1.2f;
            spriteRenderer.color = Color.yellow;
        }
    }
    void OnMouseExit()
    {
        // Reset the sprite to its original size and color when the mouse exits
        if(!isLocked){
            transform.localScale = originalScale;
            spriteRenderer.color = originalColor;
        }
    }

}
