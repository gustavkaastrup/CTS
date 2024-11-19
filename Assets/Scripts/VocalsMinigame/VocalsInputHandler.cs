using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalsInputHandler : MonoBehaviour
{
    public VocalGameLogic VocalGameLogic;

    private KeyCode CNoteKey = KeyCode.Q;
    private KeyCode ENoteKey = KeyCode.W;
    private KeyCode GNoteKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(CNoteKey))
        {
            VocalGameLogic.PlayNote(VocalGameLogic.Note.C);
        }
        if (Input.GetKeyDown(ENoteKey))
        {
            VocalGameLogic.PlayNote(VocalGameLogic.Note.E);
        }
        if (Input.GetKeyDown(GNoteKey))
        {
            VocalGameLogic.PlayNote(VocalGameLogic.Note.G);
        }
    }
}
