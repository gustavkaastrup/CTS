using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour
{
    [System.Serializable]
    public class PlayButtonPair
    {
        public PlayButton PlayerPlayButton;
        public PlayButton ReferencePlayButton;
    }

    public List<PlayButtonPair> PlayButtonPairs;
    public Button NextButton;
    public Camera Camera;

    private Color CorrectBackgroundColor = Color.green;
    private Color WrongBackgroundColor = Color.red;

    public void OnCheckButtonClick()
    {
        bool correct = true;

        foreach (PlayButtonPair playButtonPair in PlayButtonPairs)
        {
            if (
                playButtonPair.PlayerPlayButton.pitchshifterOn != playButtonPair.ReferencePlayButton.pitchshifterOn ||
                playButtonPair.PlayerPlayButton.echoOn != playButtonPair.ReferencePlayButton.echoOn ||
                playButtonPair.PlayerPlayButton.distortionOn != playButtonPair.ReferencePlayButton.distortionOn
                )
            {
                correct = false;
                break;
            }
        }

        if ( correct )
        {
            NextButton.gameObject.SetActive( true );
            Camera.backgroundColor = CorrectBackgroundColor;
        } else
        {
            Camera.backgroundColor = WrongBackgroundColor;
        }
    }

    public void OnNexelLEvelButtonClick()
    {
        Camera.backgroundColor = Color.yellow;
    }
}
