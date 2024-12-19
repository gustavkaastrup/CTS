using System.Collections.Generic;
using UnityEngine;

public class DrumInputManager : MonoBehaviour
{
    public static DrumInputManager instance;
    public GameObject kickVisual;
    public GameObject snareVisual;
    public GameObject hihatVisual;

    private Dictionary<string, DrumNoteScript> activeNotes = new Dictionary<string, DrumNoteScript>();
    private float hitWindowSeconds = 0.1f; // Adjust this value as needed

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            HandleKeyPress("C");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            HandleKeyPress("D");
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            HandleKeyPress("E");
        }
    }

    public void RegisterActiveNote(DrumNoteScript note)
    {
        if (!activeNotes.ContainsKey(note.noteName))
        {
            activeNotes[note.noteName] = note;
        }
    }

    public void UnregisterActiveNote(DrumNoteScript note)
    {
        if (activeNotes.ContainsKey(note.noteName) && activeNotes[note.noteName] == note)
        {
            activeNotes.Remove(note.noteName);
        }
    }

    private void HandleKeyPress(string noteType)
    {
        if (activeNotes.ContainsKey(noteType))
        {
            DrumNoteScript note = activeNotes[noteType];
            float timeDifference = Mathf.Abs(AudioMidiController.instance.currentTime - (float)note.targetTime);
            if (timeDifference <= hitWindowSeconds)
            {
                note.HitNote();
                activeNotes.Remove(noteType);
                switch (noteType)
                {
                    case "C":
                        kickVisual.GetComponent<PumpScript>().Pump();
                        break;
                    case "D":
                        snareVisual.GetComponent<PumpScript>().Pump();
                        break;
                    case "E":
                        hihatVisual.GetComponent<PumpScript>().Pump();
                        break;
                }
            }
            else
            {
                MissNote();
            }
        }
        else
        {
            MissNote();
        }
    }

    private void MissNote()
    {
        Debug.Log("Missed note");
        DrumScoreManager.instance.MissNote();
    }
}