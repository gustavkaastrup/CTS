using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumNoteMapperScript : MonoBehaviour
{
    public GameObject kickSpawner;
    public GameObject snareSpawner;
    public GameObject hihatSpawner;

    public static DrumNoteMapperScript instance;
    public Dictionary<int, GameObject> NoteToSpawner { get; private set; }
    public Dictionary<KeyCode, int> KeyToNote;
    void Awake()
    {
        instance = this;
        InitializeMappings();
    }
    void InitializeMappings()
    {
        NoteToSpawner = new Dictionary<int, GameObject>(){
            {36, kickSpawner},  // Kick
            {38, snareSpawner}, // Snare
            {42, hihatSpawner}  // Hihat
        };
        KeyToNote = new Dictionary<KeyCode, int>(){
            {KeyCode.A, 36},    // Kick
            {KeyCode.S, 38},    // Snare
            {KeyCode.D, 42}     // Hihat
        };
    }
}
