using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayButton : MonoBehaviour
{
    [SerializeField, Range(0, 3)] int audioClipIndex;

    [SerializeField] public bool echoOn = false;
    [SerializeField] public bool distortionOn = false;
    [SerializeField] public bool pitchshifterOn = false;
    [SerializeField] public bool lowPassFilterOn = false;

    public AudioMixer audioMixer;
    public KeyCode PlayKeyboeadLetter = KeyCode.Q;

    AudioManager audioManager;
    List<AudioClip> audioClipList;

    private void Update()
    {
        if (Input.GetKeyDown(PlayKeyboeadLetter))
        {
            Play();
        }
    }

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioClipList = new List<AudioClip>{ audioManager.BassLowE, audioManager.BassLowA, audioManager.BassMidE, audioManager.BassHighE };
    }

    private void SetEffects()
    {
        audioMixer.SetFloat("EchoWetmix", echoOn ? 100 : 0);

        audioMixer.SetFloat("DistortionLevel", distortionOn ? 0.9f : 0);
        audioMixer.SetFloat("BassMinigameVolume", distortionOn ?  -15.0f : 0);

        audioMixer.SetFloat("PitchshifterPitch", pitchshifterOn ? 1.1f : 1);

        audioMixer.SetFloat("LowpassCutoffFreq", lowPassFilterOn ? 1400 : 22000);
    }

    private void OnMouseDown()
    {
        Play();
    }

    private void Play()
    {
        SetEffects();
        //audioManager.PlaySoundEffect(audioClipList[audioClipIndex]);
        audioManager.PlaySoundEffectStopPrevious(audioClipList[audioClipIndex]);
    }
}
