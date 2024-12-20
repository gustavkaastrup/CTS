using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperControls : MonoBehaviour
{
    // Start is called before the first frame update
    public Loader loader;
    void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            loader.gameworldIndex = 1;
            Loader.Instance.LoadScene(Loader.Scene.Gameworld_Mushroom);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            loader.gameworldIndex = 2;
            Loader.Instance.LoadScene(Loader.Scene.Gameworld_Forest);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            loader.gameworldIndex = 3;
            Loader.Instance.LoadScene(Loader.Scene.Gameworld_Bar);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            loader.gameworldIndex = 4;
            Loader.Instance.LoadScene(Loader.Scene.Gameworld_Stage);
        }
    }
}
