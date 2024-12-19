using UnityEngine;
using TMPro;

public class DrumScoreManager : MonoBehaviour
{
    public static DrumScoreManager instance;
    public TextMeshProUGUI scoreText;
    public GameObject scorePopupPrefab; // Reference to the ScorePopup prefab
    public Canvas canvas; // Reference to the Canvas

    private int score;
    private int combo;
    private int streak;
    private int maxCombo = 10;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (scorePopupPrefab == null)
        {
            Debug.LogError("ScorePopupPrefab is not assigned in the DrumScoreManager.");
        }
        if (canvas == null)
        {
            Debug.LogError("Canvas is not assigned in the DrumScoreManager.");
        }
    }

    private void Update()
    {
        scoreText.text = $"Score: {score}\nCombo: {combo}\nMax Combo: {maxCombo}\nStreak: {streak}";
    }

    public void AddScore(float timeDifference)
    {
        int points = 0;

        if (Mathf.Abs(timeDifference) <= 0.01f)
        {
            points = 10; // Perfect hit
        }
        else if (Mathf.Abs(timeDifference) <= 0.5f)
        {
            points = 5; // Good hit
        }
        else if (Mathf.Abs(timeDifference) <= 0.2f)
        {
            points = 2; // Okay hit
        }

        if (points > 0)
        {
            combo++;
            streak++;
            if (combo > maxCombo)
            {
                combo = maxCombo;
            }
        }
        else
        {
            combo = 0;
            streak = 0;
        }

        score += points * combo;
        CreateScorePopup(points*combo);
        Debug.Log($"Score: {score} (Added {points * combo} points), Combo: {combo}, Max Combo: {maxCombo}");
    }

    public void MissNote()
    {
        DrumPlayerSoundManager.instance.PlayMissSound();
        ResetCombo();
    }

    public void ResetCombo()
    {
        combo = 0;
    }

    private void CreateScorePopup(int points)
    {
        if (scorePopupPrefab != null && canvas != null)
        {
            GameObject popup = Instantiate(scorePopupPrefab, canvas.transform);
            ScorePopUpScript scorePopup = popup.GetComponent<ScorePopUpScript>();
            if (scorePopup != null)
            {
                scorePopup.Initialize(points);
            }
            else
            {
                Debug.LogError("ScorePopUpScript component is missing on the scorePopupPrefab.");
            }
        }
        else
        {
            Debug.LogError("ScorePopupPrefab or Canvas is not assigned in the DrumScoreManager.");
        }
    }
}