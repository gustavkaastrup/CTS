using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalsNoteIndicator : MonoBehaviour
{
    public VocalGameLogic vocalGameLogic;
    public List<VocalsNoteStoplight> vocalsNoteStoplights;

    private void Start()
    {
        ResetNoteStoplights();
    }

    public void ResetNoteStoplights()
    {
        VocalGameLogic.Melody melody = vocalGameLogic.GetCurrentMelody();

        for (int i = 0; i < vocalsNoteStoplights.Count; i++)
        {
            VocalsNoteStoplight stoplight = vocalsNoteStoplights[i];
            stoplight.ResetNote(melody.notes[i]);
        }
    }
    public void PlayedNote(VocalGameLogic.Note note, int noteIndex)
    {
        bool correct = vocalsNoteStoplights[noteIndex].PlayNote(note);
        if (!correct)
        {
            vocalGameLogic.Failed();
            return;
        }
    }
}
