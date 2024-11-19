using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PPText2 : MonoBehaviour
{
    public string multiplier;  
        void Update()
    {
        GetComponent<Text>().text=PlayerPrefs.GetInt(multiplier)+"";
    }
}
