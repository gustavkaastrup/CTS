using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumPlayerInputManagerScript : MonoBehaviour
{
    private List<ExpandingCircleScript> _activeNotes = new List<ExpandingCircleScript>();
    public static DrumPlayerInputManagerScript instance;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        foreach (var key in DrumNoteMapperScript.instance.KeyToNote.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                CheckKeyPress(key);
                print("key pressed: " + key);
            }
        }
    }

    public void RegisterNote(ExpandingCircleScript note)
    {
        _activeNotes.Add(note);
    }
    public void UnregisterNote(ExpandingCircleScript note)
    {
        _activeNotes.Remove(note);
    }
    void CheckKeyPress(KeyCode key)
    {
        foreach (var note in _activeNotes)
        {
            note.TryHit(key);
        }
    }
}
