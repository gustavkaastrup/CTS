using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VocalsPlayButton : MonoBehaviour
{
    public VocalGameLogic VocalGameLogic;
    
    public List<VocalGameLogic.Note> notes;

    private List<AudioClip> melodyAudioClips;

    void Start()
    {
        melodyAudioClips = new List<AudioClip>();

        foreach (VocalGameLogic.Note note in notes)
        {
            AudioClip audioClip = VocalGameLogic.GetAudioClipFromNote(note);
            if (audioClip != null)
            {
                melodyAudioClips.Add(audioClip);
            }
        }
    }

    private void OnMouseDown()
    {
        StartCoroutine(VocalGameLogic.audioManager.PlayAudioInSequence(melodyAudioClips));
        VocalGameLogic.audioPlayed = true;
    }
}
