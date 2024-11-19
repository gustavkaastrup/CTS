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

    public bool PlayNote(VocalGameLogic.Note note)
    {
        Color noteColor = (note == CorrectNote) ? Color.green : Color.red;
        
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
}
