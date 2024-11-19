using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalsNoteIndicator : MonoBehaviour
{
    public VocalGameLogic vocalGameLogic;
    public List<VocalsNoteStoplight> vocalsNoteStoplights;

    private int stoplightIndex = 0;

    private void Start()
    {
        InitializeNoteStoplights();
    }

    private void InitializeNoteStoplights()
    {
        VocalGameLogic.Melody melody = vocalGameLogic.GetCurrentMelody();

        for (int i = 0; i < vocalsNoteStoplights.Count; i++)
        {
            vocalsNoteStoplights[i].CorrectNote = melody.notes[i];
        }
    }
    public void PlayedNote(VocalGameLogic.Note note)
    {
        bool correct = vocalsNoteStoplights[stoplightIndex].PlayNote(note);
        if (!correct)
        {
            vocalGameLogic.Failed();
        }
        stoplightIndex++;
        if (stoplightIndex ==  vocalsNoteStoplights.Count)
        {
            vocalGameLogic.Passed();
        }
    }
}
