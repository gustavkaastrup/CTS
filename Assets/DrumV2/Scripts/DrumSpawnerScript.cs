using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSpawnerScript : MonoBehaviour
{
    public GameObject kickPrefab;
    public GameObject snarePrefab;
    public GameObject hihatPrefab;

    public List<MidiNote> midiNotes = new List<MidiNote>();

    public float KickRadius = 2.0f;
    public float SnareRadius = 4.0f;
    public float HihatRadius = 6.0f;
    private float currentTime => AudioMidiController.instance.currentTime;
    private int currentBar => AudioMidiController.instance.currentBar;
    private int barIndex = 0;

    private float barLength => AudioMidiController.instance.GetBarLength();
    private int lastProcessedBar = -1;
    private Dictionary<int, List<GameObject>> spawnedNotes = new Dictionary<int, List<GameObject>>();

    public void Start()
    {
        AudioMidiController audioMidiController = AudioMidiController.instance;
        midiNotes = audioMidiController.GetMidiNotes();
        Debug.Log("Starting DrumSpawnerScript");
        SpawnNotes(currentBar);
    }

    void Update()
    {
        if(currentBar != lastProcessedBar)
        {
            SpawnNotesForUpcomingBar();
            lastProcessedBar = currentBar;
        }
    }
    
    void SpawnNotesForUpcomingBar()
    {
        Debug.Log("Spawning notes for upcoming bar");
        while (barIndex <= currentBar + 1)
        {
            Debug.Log($"Spawning notes for bar index: {barIndex}");
            SpawnNotes(barIndex);
            barIndex++;
        }
    }

    public void SpawnNotes(int barIndex)
    {
        double barStartTime = barIndex * barLength;
        double barEndTime = barStartTime + barLength;
        foreach(var note in midiNotes){
            if(note.Time >= barStartTime-0.05 && note.Time < barEndTime-0.05f){
                double timeInBar = note.Time - barStartTime;
                float angle = (float)(timeInBar / barLength) * 360;
                Vector3 pos = GetPositionOnCircle(angle, GetRadiusForNoteName(note.NoteName));
                GameObject noteObject = Instantiate(GetPrefabForNoteName(note.NoteName), pos, Quaternion.identity);
                noteObject.name = $"Note_{note.NoteName}_Bar{barIndex}_Beat{timeInBar}";
                DrumNoteScript noteScript = noteObject.AddComponent<DrumNoteScript>();
                noteScript.Initialize(note.NoteName, barIndex, (int)timeInBar, note.Time);

                if (barIndex == currentBar)
                {
                    noteScript.SetActive();
                    Debug.Log($"Spawned and activated note: {noteObject.name} at position {pos}");
                }
                else
                {
                    noteScript.SetInactive();
                    Debug.Log($"Spawned and set inactive note: {noteObject.name} at position {pos}");
                }
            }
        }
    }

    Vector3 GetPositionOnCircle(float angle, float radius)
    {
        return new Vector3(
            Mathf.Cos((float)angle * Mathf.Deg2Rad) * radius, 
            Mathf.Sin((float)angle * Mathf.Deg2Rad) * radius, 
            0);
    }

    GameObject GetPrefabForNoteName(string noteName)
    {
        switch (noteName)
        {
            case "C":
                return kickPrefab;
            case "D":
                return snarePrefab;
            case "E":
                return hihatPrefab;
            default:
                Debug.LogWarning($"No prefab found for note name: {noteName}");
                return null;
        }
    }

    float GetRadiusForNoteName(string noteName)
    {
        switch (noteName)
        {
            case "C":
                return KickRadius;
            case "D":
                return SnareRadius;
            case "E":
                return HihatRadius;
            default:
                Debug.LogWarning($"No radius found for note name: {noteName}");
                return 0f;
        }
    }
}