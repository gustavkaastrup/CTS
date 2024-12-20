using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStopManagerScript : MonoBehaviour
{
    AudioMidiController audioMidiController;
    public bool gameEnded = false;
    public int RequiredScore;
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
            gameEnded = true;
            if (DrumScoreManager.instance.score >= RequiredScore)
            {
                SFXPlayerScript.instance.PlayNextLevel();
                Loader.Instance.LevelSuccess();
            }
        }
    }

    
}
