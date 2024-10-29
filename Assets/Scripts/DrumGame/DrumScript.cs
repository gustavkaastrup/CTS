using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 originalScale;
    public SpriteRenderer drumSprite;
    public KeyCode key;
    public AudioSource sound;
    public Transform center;
    private bool active = false;
    bool isPressed = false;

    void Start()
    {
        originalScale = transform.localScale;
        drumSprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        center = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(key))
            {
                StartCoroutine(ScaleSprite());
                sound.Play();
                isPressed = true;
            }
        }

    }

    public IEnumerator ScaleSprite()
    {
        Vector3 targetScale = originalScale * 1.5f;
        float duration = 0.2f; // Time to scale up
        float elapsedTime = 0f;

        // Scale up
        transform.localScale = targetScale;

        // Scale down
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rotator")
        {
            active = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rotator")
        {
            active = false;
        }
        if (isPressed)
        {
            isPressed = false;
            Debug.Log(gameObject.name + "Drum hit!");
        }
        else
        {
            Debug.Log(gameObject.name + "Drum missed!");
        }
    }
}


