using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PPText : MonoBehaviour
{
    public string scoreText;  
        void Update()
    {
        GetComponent<Text>().text=PlayerPrefs.GetInt(scoreText)+"";
    }
}
