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
    string path = Application.dataPath + "/GuitarAssets/StreamingAssets/" + fileLocation;
    midiFile = MidiFile.Read(path);
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
    Debug.Log("Audio will start after a delay of: " + songDelayInSeconds + " seconds.");
    audioSource.PlayDelayed(songDelayInSeconds); // Delays the song start
}

    void Update()
    {
    }
     private void OnSongEnd()
    {
        int currentScore = PlayerPrefs.GetInt("Score", 0);
        Debug.Log("MIDI File Finished! Final Score: " + currentScore);

        if (currentScore >= targetScore)
        {
            loader.LevelSuccess();
            Debug.Log("Congratulations! You met the target score of " + targetScore);
        }
        else
        {
            loader.LevelFailed();
            Debug.Log("Better luck next time! Target score was: " + targetScore);
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
        Debug.Log("All notes have been spawned.");
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
