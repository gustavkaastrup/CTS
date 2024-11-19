using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VocalsPlayButton : MonoBehaviour
{
    
    public VocalGameLogic vocalGameLogic;
    //public List<VocalGameLogic.Melody> melodiesNotes;

    private List<List<AudioClip>> melodiesAudioClips;

    void Start()
    {
        InitilizeMelodiesAudioClips();
    }

    private void InitilizeMelodiesAudioClips()
    {
        melodiesAudioClips = new List<List<AudioClip>>();

        foreach (VocalGameLogic.Melody melody in vocalGameLogic.melodies)
        {
            //melodiesAudioClips.Add(new List<AudioClip>());
            List<AudioClip> melodyAudioClips = new List<AudioClip>();

            foreach (VocalGameLogic.Note note in melody.notes)
            {
                AudioClip audioClip = VocalGameLogic.GetAudioClipFromNote(note);
                if (audioClip != null)
                {
                    melodyAudioClips.Add(audioClip);
                }
            }

            melodiesAudioClips.Add(melodyAudioClips);
        }
    }

    private void OnMouseDown()
    {
        if (!vocalGameLogic.IsRunning())
        {
            return;
        }

        List<AudioClip> melodyAudioClips = melodiesAudioClips[vocalGameLogic.GetMelodyIndex()];
        StartCoroutine(VocalGameLogic.audioManager.PlayAudioInSequence(melodyAudioClips));
        vocalGameLogic.audioPlayed = true;
    }
}
