using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumNoteSpawnerScript : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject SpawnNoteWithParameters(int targetSample, DrumMasterTimingScript timingScript, int noteNumber)
    {
        GameObject noteObject = Instantiate(notePrefab, transform.position, Quaternion.identity);
        ExpandingCircleScript circle = noteObject.GetComponent<ExpandingCircleScript>();

        if (circle != null)
        {
            circle.Initialize(targetSample, timingScript, noteNumber);
        }
        else
        {
            Debug.LogError("ExpandingCircleScript not found on spawned note object.");
        }

        return noteObject;
    }
}
