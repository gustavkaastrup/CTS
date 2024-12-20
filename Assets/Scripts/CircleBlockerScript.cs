using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBlockerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public Vector3 originalPos;
    public int GameLevelIndex;
    void Start()
    {
        originalPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (GameLevelIndex <= Loader.Instance.GetGameworldIndex())
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
        originalPos.z = -5;
        transform.position = originalPos;
    }
    void UnlockCircle()
    {
        spriteRenderer.enabled = false;
    }
}
