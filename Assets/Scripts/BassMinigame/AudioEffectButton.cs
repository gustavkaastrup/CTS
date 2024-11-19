using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEffectButton : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject playButton;

    public enum EffectType
    {
        Echo,
        Distortion,
        Pitchshifter
    }
    public EffectType effectType = EffectType.Echo;

    bool effectOn = false;
    PlayButton playButtonScript;


    private Color onColor = Color.black;
    private Color offColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = offColor;
        playButtonScript = playButton.GetComponent<PlayButton>();
    }

    private void OnMouseDown()
    {
        effectOn = !effectOn;
        GetComponent<SpriteRenderer>().color = effectOn ? onColor : offColor;


        switch (effectType)
        {
            case EffectType.Echo:
                playButtonScript.echoOn = effectOn;
                break;
            case EffectType.Distortion:
                playButtonScript.distortionOn = effectOn;
                break;
            case EffectType.Pitchshifter:
                playButtonScript.pitchshifterOn = effectOn;
                break;
        }
    }
}