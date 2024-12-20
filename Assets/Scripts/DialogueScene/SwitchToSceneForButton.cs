using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToSceneForButton : MonoBehaviour
{
    public Loader.Scene scene;
    private Loader loader;

    private void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
    }

    public void LoadChosenScene()
    {
        loader.LoadScene(scene);
    }
}
