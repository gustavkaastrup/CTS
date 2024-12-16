using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalsNoteIndicator : MonoBehaviour
{
    public VocalGameLogic vocalGameLogic;
    public GameObject stoplightPrefab;

    private List<VocalsNoteStoplight> vocalsNoteStoplights;
    private List<GameObject> currentStoplights;

    private void Start()
    {
        currentStoplights = new List<GameObject>();
        ResetNoteStoplights();
    }

    public void ResetNoteStoplights()
    {
        InitiateStoplights();

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

    private void InitiateStoplights()
    {
        foreach (GameObject stoplight in currentStoplights)
        {
            Destroy(stoplight);
        }

        currentStoplights = new List<GameObject>();
        vocalsNoteStoplights = new List<VocalsNoteStoplight>();
        int stoplightPositionX = -4;

        foreach (VocalGameLogic.Note note in vocalGameLogic.GetCurrentMelody().notes)
        {
            GameObject spawnedStoplight = Instantiate(stoplightPrefab, new Vector3(stoplightPositionX, 0, 0), Quaternion.identity);
            vocalsNoteStoplights.Add(spawnedStoplight.GetComponent<VocalsNoteStoplight>());
            currentStoplights.Add(spawnedStoplight);
            stoplightPositionX++;
        }
    }
}
