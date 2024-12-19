using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public float maxScoreDistance = 2.0f;
    public int[] streakThresholds = { 3, 6, 9, 12 }; // Default streak thresholds
    public int maxMultiplier = 2; // Default max multiplier

    private int currentMultiplier = 1;
    private int hitStreak = 0;

    public int CalculateScore(Vector2 notePosition, Vector2 activatorCenter)
    {
        float distance = Vector2.Distance(notePosition, activatorCenter);
        int baseScore = 0;

        if (distance <= 0.2f)
        {
            baseScore = 100;
            PlayerPrefs.SetString("Hit", "Perfect");
        }
        else if (distance <= 0.5f)
        {
            baseScore = 75;
            PlayerPrefs.SetString("Hit", "Great");
        }
        else if (distance <= .8f)
        {
            baseScore = 50;
            PlayerPrefs.SetString("Hit", "Good");
        }
        else if (distance <= maxScoreDistance)
        {
            baseScore = 25;
            PlayerPrefs.SetString("Hit", "Barely Hit");
        }
        else
        {
            ResetMultiplier();
            PlayerPrefs.SetString("Hit", "Miss");
            return 0;
        }

        int finalScore = baseScore * currentMultiplier;
        UpdateMultiplier(baseScore);
        return finalScore;
    }

    private void UpdateMultiplier(int score)
    {
        if (score >= 50) // Count as a successful hit
        {
            hitStreak++;

            foreach (int threshold in streakThresholds)
            {
                if (hitStreak == threshold && currentMultiplier < maxMultiplier)
                {
                    currentMultiplier++;
                    PlayerPrefs.SetInt("Multiplier", currentMultiplier);
                    break;
                }
            }
        }
        else
        {
            ResetMultiplier();
        }
    }

    private void ResetMultiplier()
    {
        hitStreak = 0;
        currentMultiplier = 1;
        PlayerPrefs.SetInt("Multiplier", 1);
    }
}