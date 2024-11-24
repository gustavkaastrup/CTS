using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMinigameButton : MonoBehaviour
{
    public Loader.Scene minigameLevelScene;

    private Loader loader;
    void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
    }

    public void OnPlayMinigameButtonClick()
    {
        loader.LoadScene(minigameLevelScene);
    }
}
