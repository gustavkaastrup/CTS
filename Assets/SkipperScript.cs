using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkipperScript : MonoBehaviour
{
    Loader loader;
    private void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            loader.LoadScene(Loader.Scene.Gameworld);
        }
    }
}
