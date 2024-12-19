using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI feedbackText;

    private Loader loader;

    int feedbackIndex = 0;
    private string[] feedbackIncorectMessages =
    {
        "some effects arent correct, try again!",
        "try changing some effects :)",
        "listen carefully and try to find correct effects"
    };

    public void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
    }

    public void OnCheckButtonClick()
    {
        bool correct = true;

        foreach (PlayButtonPair playButtonPair in PlayButtonPairs)
        {
            if (
                playButtonPair.PlayerPlayButton.pitchshifterOn != playButtonPair.ReferencePlayButton.pitchshifterOn ||
                playButtonPair.PlayerPlayButton.echoOn != playButtonPair.ReferencePlayButton.echoOn ||
                playButtonPair.PlayerPlayButton.distortionOn != playButtonPair.ReferencePlayButton.distortionOn ||
                playButtonPair.PlayerPlayButton.lowPassFilterOn != playButtonPair.ReferencePlayButton.lowPassFilterOn
                )
            {
                correct = false;
                break;
            }
        }

        if ( correct )
        {
            NextButton.gameObject.SetActive( true );
        } else
        {
            feedbackText.text = feedbackIncorectMessages[feedbackIndex].ToString();
            feedbackIndex = (feedbackIndex + 1) % feedbackIncorectMessages.Length;
        }
    }

    public void OnNexelLEvelButtonClick()
    {
        loader.LevelSuccess();
    }
}
