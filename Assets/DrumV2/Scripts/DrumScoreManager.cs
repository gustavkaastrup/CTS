using UnityEngine;

public class DrumScoreManager : MonoBehaviour
{
    public static DrumScoreManager instance;
    private int score = 0;
    private int combo = 0;
    private int maxCombo = 0;

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

    public void AddScore(float timeDifference)
    {
        int points = 0;

        if (Mathf.Abs(timeDifference) <= 0.05f)
        {
            points = 100; // Perfect hit
        }
        else if (Mathf.Abs(timeDifference) <= 0.1f)
        {
            points = 50; // Good hit
        }
        else if (Mathf.Abs(timeDifference) <= 0.2f)
        {
            points = 20; // Okay hit
        }

        if (points > 0)
        {
            combo++;
            if (combo > maxCombo)
            {
                maxCombo = combo;
            }
        }
        else
        {
            combo = 0;
        }

        score += points * combo;
        Debug.Log($"Score: {score} (Added {points * combo} points), Combo: {combo}, Max Combo: {maxCombo}");
    }

    public void ResetCombo()
    {
        combo = 0;
    }
}