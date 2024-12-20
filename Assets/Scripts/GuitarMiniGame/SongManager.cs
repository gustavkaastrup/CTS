using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public int targetScore; 

    public int inputDelayInMilliseconds;

    private int finishedLanes = 0;

    public int amountOfLanes;
    public string fileLocation;
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
     private Loader loader;
    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    public static MidiFile midiFile;
    void Start()
    {
        Instance = this;
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
    }
    void Awake () {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
    }
    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }

   private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }
    
    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        StartSong();
    }
   public void StartSong()
{
    audioSource.PlayDelayed(songDelayInSeconds); // Delays the song start
}

    void Update()
    {
    }
     private void OnSongEnd()
    {
        int currentScore = PlayerPrefs.GetInt("Score", 0);

        if (currentScore >= targetScore)
        {
            loader.LevelSuccess();
        }
        else
        {
            loader.LevelFailed();
        }
    }

      public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    public void LaneFinishedSpawning()
{
    finishedLanes++;
    Debug.Log (finishedLanes);

    if (finishedLanes == amountOfLanes) 
    {
        StartCoroutine(DelayOnSongEnd());
    }
}

private IEnumerator DelayOnSongEnd()
{
    float delay = 3.0f; 
    yield return new WaitForSeconds(delay);
    OnSongEnd();
}

}
