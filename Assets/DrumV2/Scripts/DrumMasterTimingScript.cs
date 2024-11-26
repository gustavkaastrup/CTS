using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumMasterTimingScript : MonoBehaviour
{
    static public DrumMasterTimingScript instance;
    private AudioSource _audioSource;
    public int sampleRate;

    private void Awake()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
        sampleRate = AudioSettings.outputSampleRate;
    }
    public float CurrentTimeInSeconds => (float)_audioSource.timeSamples / sampleRate;
    public int CurrentSample => _audioSource.timeSamples;

    public void Play()
    {
        _audioSource.Play();
    }
}
