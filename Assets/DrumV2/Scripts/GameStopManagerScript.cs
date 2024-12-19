using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStopManagerScript : MonoBehaviour
{
    AudioMidiController audioMidiController;
    public bool gameEnded = false;
    void Start()
    {
        audioMidiController = AudioMidiController.instance;
    }

    void Update()
    {
        if(audioMidiController.GetComponent<AudioSource>().isPlaying == false && gameEnded == false)
        {
            Debug.Log("Game Over");
            SFXPlayerScript.instance.PlayGameOver();
            Time.timeScale = 0;
            gameEnded = true;
        }
    }
}
