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
    public Transform activatorCenter;  
    public float maxScoreDistance = 2.0f;  
    public ScoreCalculator scoreCalculator; 


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
            activatorCenter = this.transform;  
        }
        if (scoreCalculator == null)
        {
            scoreCalculator = FindObjectOfType<ScoreCalculator>();
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
            if (note != null)
            {
                int score = scoreCalculator.CalculateScore(note.transform.position, activatorCenter.position);
                AddScore(score);
            }

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