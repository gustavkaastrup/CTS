using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class deprecated_DrumScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 originalScale;
    public SpriteRenderer drumSprite;
    public KeyCode key;
    public AudioSource sound;
    public Transform center;
    private deprecated_DrumGameManagerScript drumManager = deprecated_DrumGameManagerScript.instance;

    private TMP_Text scoreText;
    private bool active = false;
    bool isPressed = false;

    public GameObject col;

    public float distance;
    public float scoreMultiplier;
    // Add score based on the distance
    public int scoreToAdd;
    void Start()
    {
        originalScale = transform.localScale;
        drumSprite = GetComponent<SpriteRenderer>();
        center = transform;
        scoreText = GameObject.FindWithTag("Countdown").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(key) && !isPressed)
            {
                StartCoroutine(ScaleSprite());
                isPressed = true;

                Collider2D collider = col.GetComponent<Collider2D>();
                Vector3 closestPoint = collider.ClosestPoint(center.position);
                distance = Vector3.Distance(center.position, closestPoint);
                if (distance < 0.05)
                {
                    scoreToAdd = 10;
                }
                else if (distance < 0.5)
                {
                    scoreToAdd = 5;
                }
                else
                {
                    scoreToAdd = 1;
                }
                drumManager.AddScore(scoreToAdd);
                StartCoroutine(PointAnimation());
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
            col = other.gameObject;
            active = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rotator")
        {
            active = false;
            isPressed = false;
            col = null;
        }
    }

    public IEnumerator PointAnimation()
    {
        scoreText.text = "+" + scoreToAdd;
        scoreText.color = Color.green;
        scoreText.rectTransform.anchoredPosition = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        scoreText.text = "";
    }
}


