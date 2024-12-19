using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStopManagerScript : MonoBehaviour
{
    AudioMidiController audioMidiController;

    void Start()
    {
        audioMidiController = AudioMidiController.instance;
    }

    void Update()
    {
        if(audioMidiController.GetComponent<AudioSource>().isPlaying == false && audioMidiController.currentBar > 0)
        {
            Debug.Log("Game Over");
            SFXPlayerScript.instance.PlayGameOver();
            Time.timeScale = 0;
        }
    }
}
