using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VocalsPlayButton : MonoBehaviour
{
    
    public VocalGameLogic vocalGameLogic;

    private void OnMouseDown()
    {
        vocalGameLogic.PlayCurrentMelody();
    }
}
