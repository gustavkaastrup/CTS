using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BarAmbience : MonoBehaviour
{
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;

    AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.SetMusicVolume(musicVolume);
        audioManager.PlayMusic(audioManager.BarAmbience);
    }
}
