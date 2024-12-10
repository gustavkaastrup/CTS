using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VocalGameLogic : MonoBehaviour
{
    public bool audioPlayed = false;
    public static AudioManager audioManager;
    public VocalsNoteIndicator noteIndicator;

    public enum Note
    {
        C,
        E,
        G
    }
    [System.Serializable]
    public class Melody
    {
        public List<Note> notes;
    }

    public List<Melody> melodies;
    public List<VocalsNoteIndicator> vocalsNoteIndicators;

    private int melodyIndex = 0;
    private int noteIndex = 0;
    private bool isRunning = true;
    private List<AudioClip> melodyAudioClips;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayNote(Note note)
    {
        AudioClip clip = GetAudioClipFromNote(note);
        if (clip != null)
        {
            audioManager.PlaySoundEffectStopPrevious(clip);
        }

        noteIndicator.PlayedNote(note, noteIndex);

        if (audioPlayed)
        {
            // TODO
        }
        noteIndex++;
        if (noteIndex >= melodies[melodyIndex].notes.Count)
        {
            noteIndex = 0;
            melodyIndex++;
            if (melodyIndex >= melodies.Count)
            {
                isRunning = false;
            } else
            {
                noteIndicator.ResetNoteStoplights();
            }
        }
    }

    public static AudioClip GetAudioClipFromNote(Note note)
    {
        switch (note)
        {
            case VocalGameLogic.Note.C:
                return audioManager.VocalsC;
            case VocalGameLogic.Note.E:
                return audioManager.VocalsE;
            case VocalGameLogic.Note.G:
                return audioManager.VocalsG;
            default:
                Debug.Log("Vocals: Note to audio clip returned null");
                return null;
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }
    public void Failed()
    {
        //failed = true;
        //Camera.main.backgroundColor = Color.gray;
    }
    public void Passed()
    {
        //Camera.main.backgroundColor = Color.green;
    }
    public int GetMelodyIndex()
    {
        return melodyIndex;
    }
    public Melody GetCurrentMelody()
    {
        return melodies[melodyIndex];
    }
}
