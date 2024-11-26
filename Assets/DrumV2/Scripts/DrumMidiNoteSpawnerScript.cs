using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using System.Linq;

public class DrumMidiNoteSpawnerScript : MonoBehaviour
{
    public static DrumMidiNoteSpawnerScript instance;

    private void Awake()
    {
        instance = this;
    }

    public float leadTimeInSeconds = 1f;

    public void SpawnMidiNotes(MidiFile midiFile)
    {
        var tempoMap = midiFile.GetTempoMap();
        var drumNotes = midiFile.GetNotes().ToList();
        Debug.Log("Total drum notes found: " + drumNotes.Count);

        foreach (var note in drumNotes)
        {
            var metricTime = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
            float targetHitTime = (float)metricTime.TotalSeconds;
            int targetSample = Mathf.RoundToInt(targetHitTime * DrumMasterTimingScript.instance.sampleRate);
            int spawnSample = targetSample - Mathf.RoundToInt(leadTimeInSeconds * DrumMasterTimingScript.instance.sampleRate);

            Debug.Log("Note: " + note.NoteNumber + " Target Hit Time: " + targetHitTime + " Target Sample: " + targetSample + " Spawn Sample: " + spawnSample);

            if (DrumNoteMapperScript.instance.NoteToSpawner.TryGetValue(note.NoteNumber, out GameObject spawner))
            {
                StartCoroutine(ScheduleNoteSpawn(spawner, spawnSample, targetSample, note.NoteNumber));
            }
            else
            {
                Debug.LogWarning("No spawner found for note number: " + note.NoteNumber);
            }
        }
    }

    IEnumerator ScheduleNoteSpawn(GameObject spawner, int sample, int targetSample, int noteNumber)
    {
        while (DrumMasterTimingScript.instance.CurrentSample < sample)
        {
            yield return null;
        }

        var spawnerScript = spawner.GetComponent<DrumNoteSpawnerScript>();
        if (spawnerScript != null)
        {
            spawnerScript.SpawnNoteWithParameters(targetSample, DrumMasterTimingScript.instance, noteNumber);
        }
        else
        {
            Debug.LogError("DrumNoteSpawnerScript not found on spawner object.");
        }
    }
}