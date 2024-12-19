using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    private Loader loader;
    void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
    }

    public void OnBackButtonClick()
    {
        loader.LoadLastScene();
    }
}
