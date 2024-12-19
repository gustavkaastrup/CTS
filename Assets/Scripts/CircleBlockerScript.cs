using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBlockerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public int GameLevelIndex;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (GameLevelIndex <= Loader.Instance.completeGameWorldIndex)
        {
            UnlockCircle();
        } else
        {
            LockCircle();
        }
    }

    // Update is called once per frame

    void LockCircle()
    {
        spriteRenderer.enabled = true;
    }
    void UnlockCircle()
    {
        spriteRenderer.enabled = false;
    }
}
