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


    void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }

    void Start(){
        old = sr.color;
        PlayerPrefs.SetInt("Score", 0);
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
    if (distance <= 0.2f)
    {
        AddScore(100);  // Perfect score
        Debug.Log("Perfect Hit! Distance: " + distance);
    }
    else if (distance <= 0.5f)
    {
        AddScore(75);   // Great score
        Debug.Log("Great Hit! Distance: " + distance);
    }
    else if (distance <= 0.7f)
    {
        AddScore(50);   // Good score
        Debug.Log("Good Hit! Distance: " + distance);
    }
    else if (distance <= maxScoreDistance)
    {
        AddScore(25);   // Barely hit, fewer points
        Debug.Log("Barely Hit! Distance: " + distance);
    }
    else
    {
        AddScore(0);    // Missed
        Debug.Log("Missed! Distance: " + distance);
    }
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
    void AddScore(int score){
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")+score);
    }

    IEnumerator Pressed(){
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        sr.color = old;
    }
}
