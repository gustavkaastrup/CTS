using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalsNoteStoplight : MonoBehaviour
{
    public VocalGameLogic VocalGameLogic;
    public VocalGameLogic.Note CorrectNote;

    public SpriteRenderer Note_C;
    public SpriteRenderer Note_E;
    public SpriteRenderer Note_G;

    private Color correctColor = Color.green;
    private Color wrongColor = Color.red;
    private Color neutralColor = Color.grey;

    public bool PlayNote(VocalGameLogic.Note note)
    {
        Color noteColor = (note == CorrectNote) ? correctColor : wrongColor;
        
        switch(note)
        {
            case VocalGameLogic.Note.C:
                Note_C.color = noteColor;
                break;
            case VocalGameLogic.Note.E:
                Note_E.color = noteColor;
                break;
            case VocalGameLogic.Note.G:
                Note_G.color = noteColor;
                break;
        }

        return note == CorrectNote;
    }

    public void ResetNote(VocalGameLogic.Note correctNote)
    { 
        CorrectNote = correctNote;
        Note_C.color = neutralColor;
        Note_E.color = neutralColor;
        Note_G.color = neutralColor;
    }
}
