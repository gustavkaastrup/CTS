using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;  
    bool active = false;   
    GameObject note;
    Color old; 
    public Transform activatorCenter;  // The center point of the activator for accuracy calculation
    public float maxScoreDistance = 1.0f;  // Max distance for perfect score

    // Multiplier and streak tracking
    private int currentMultiplier = 1;
    private int hitStreak = 0;
    private const int maxMultiplier = 2; // Limit the multiplier
    private int[] streakThresholds = { 3, 6, 9, 12 }; // Hit streaks to increase multiplier

    void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }

    void Start(){
        old = sr.color;
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Multiplier", 1); 
        PlayerPrefs.SetString("Hit", ""); 

        if (activatorCenter == null)
        {
            activatorCenter = this.transform;  // Use the current GameObject's position as the center
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(key)){
            StartCoroutine(Pressed());  
        }

        if (Input.GetKeyDown(key) && active)
        {
            Destroy(note);
            CalculateScore();
        }
    }

    void CalculateScore()
    {
        float distance = Vector2.Distance(note.transform.position, activatorCenter.position);

        // Define your score thresholds
        int baseScore = 0;

        if (distance <= 0.2f)
        {
            baseScore = 100;  
             PlayerPrefs.SetString("Hit", "Perfect"); 
            Debug.Log("Perfect Hit! Distance: " + distance);
        }
        else if (distance <= 0.5f)
        {
            baseScore = 75;   // Great score
            PlayerPrefs.SetString("Hit", "Great"); 
            Debug.Log("Great Hit! Distance: " + distance);
        }
        else if (distance <= 0.7f)
        {
            baseScore = 50;   // Good 
            PlayerPrefs.SetString("Hit", "Good"); 
            Debug.Log("Good Hit! Distance: " + distance);
        }
        else if (distance <= maxScoreDistance)
        {
            baseScore = 25;   // Barely hit, fewer points
            PlayerPrefs.SetString("Hit", "Barely Hit");
            Debug.Log("Barely Hit! Distance: " + distance);
        }
        else
        {
            ResetMultiplier(); // Missed
            Debug.Log("Missed! Distance: " + distance);
            return;
        }

        // Apply multiplier to score
        int finalScore = baseScore * currentMultiplier;
        AddScore(finalScore);

        // Update streak and check multiplier
        UpdateMultiplier(baseScore);
    }

    void UpdateMultiplier(int score)
    {
        if (score >= 50) // Count as a successful hit
        {
            hitStreak++;

            // Increase multiplier if hit streak reaches thresholds
            for (int i = 0; i < streakThresholds.Length; i++)
            {
                if (hitStreak == streakThresholds[i] && currentMultiplier < maxMultiplier)
                {
                    currentMultiplier++;
                    PlayerPrefs.SetInt("Multiplier", currentMultiplier); // Optional
                    Debug.Log("Multiplier Increased! New Multiplier: " + currentMultiplier);
                    break;
                }
            }
        }
        else
        {
            ResetMultiplier(); // Lower scores reset the streak
        }
    }

    void ResetMultiplier()
    {
        hitStreak = 0;
        currentMultiplier = 1;
        PlayerPrefs.SetInt("Multiplier", 1); // Optional
        Debug.Log("Multiplier Reset.");
    }

    void OnTriggerEnter2D(Collider2D col)  
    {
        active = true;
        if (col.gameObject.tag == "Note")  
        { 
            note = col.gameObject; 
        }
    }

    void OnTriggerExit2D(Collider2D col)  
    {
        active = false;       
    }

    void AddScore(int score)
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + score);
    }

    IEnumerator Pressed()
    {
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        sr.color = old;
    }
}