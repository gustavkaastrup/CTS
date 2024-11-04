using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConductorScript : MonoBehaviour
{
    public float bpm;
    public float secPerBeat;
    public float songPosition;
    public float songPosInBeats;
    public float dspSongTime;
    public float firstBeatOffset;

    private DrumGameManagerScript drumManager;

    public float beatsPerLoop;

    public int completedLoops = 0;
    public float loopPositionInBeats;

    public bool songPlaying = false;

    public float loopPositionInAnalog;
    private bool gameOver = false;
    public AudioSource musicSource;

    public static ConductorScript instance;


    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        secPerBeat = 60f / bpm;
        drumManager = DrumGameManagerScript.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (musicSource.isPlaying)
        {
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
            songPosInBeats = songPosition / secPerBeat;
            if (songPosInBeats >= (completedLoops + 1) * beatsPerLoop)
            {
                completedLoops++;
            }
            loopPositionInBeats = songPosInBeats - completedLoops * beatsPerLoop;
            loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
        }
        if (!musicSource.isPlaying && songPosInBeats > 0)
        {
            if (!gameOver)
            {
                drumManager.GameOver();
                gameOver = true;
            }
        }
    }

    public void StartSong()
    {
        musicSource.Play();
        dspSongTime = (float)AudioSettings.dspTime;
    }
}
