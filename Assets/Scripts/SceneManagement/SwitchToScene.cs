using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Loader.Scene scene;
    private Loader loader;

    private void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
    }

    private void OnMouseDown()
    {
        loader.LoadScene(scene);
    }
}
