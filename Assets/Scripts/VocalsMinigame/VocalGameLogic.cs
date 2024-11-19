using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VocalGameLogic : MonoBehaviour
{
    public bool audioPlayed = false;
    public AudioMixer audioMixer;
    public AudioManager audioManager;

    public enum Note
    {
        C,
        E,
        G
    }

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

        if (audioPlayed)
        {
            // TODO
        }
    }

    public AudioClip GetAudioClipFromNote(Note note)
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
}
