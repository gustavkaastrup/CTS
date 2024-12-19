using TMPro;
using UnityEngine;

public class ScorePopUpScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float duration = 1.0f; // Duration of the popup
    public float moveDistance = 1.0f; // Distance the popup moves
    public float fadeDuration = 0.5f; // Duration of the fade out
    public float offsetRange = 50f; // Range for random offset

    private float timer;

    public void Initialize(int points)
    {
        scoreText.text = points.ToString();
        timer = duration;

        // Set the position to the center of the screen with a random offset
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = new Vector2(
            Random.Range(-offsetRange, offsetRange),
            Random.Range(-offsetRange, offsetRange)
        );
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            // Move the popup upwards
            transform.position += Vector3.up * (moveDistance / duration) * Time.deltaTime;

            // Fade out the popup
            if (timer <= fadeDuration)
            {
                Color color = scoreText.color;
                color.a = timer / fadeDuration;
                scoreText.color = color;
            }
        }
    }
}