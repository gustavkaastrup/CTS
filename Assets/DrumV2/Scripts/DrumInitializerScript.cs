using System.Collections;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using UnityEngine;
using System.IO;

public class DrumInitializerScript : MonoBehaviour
{
    DrumMasterTimingScript _drumMasterTimingScript;
    DrumMidiNoteSpawnerScript _drumMidiNoteSpawnerScript;
    public string midiFilePath;


    IEnumerator Start()
    {
        yield return null;

        midiFilePath = Application.streamingAssetsPath + "/" + midiFilePath;
        _drumMasterTimingScript = DrumMasterTimingScript.instance;
        _drumMidiNoteSpawnerScript = DrumMidiNoteSpawnerScript.instance;

        if (_drumMasterTimingScript == null)
        {
            Debug.LogError("DrumMasterTimingScript not found");
            yield break;
        }
        if (_drumMidiNoteSpawnerScript == null)
        {
            Debug.LogError("DrumMidiNoteSpawnerScript not found");
            yield break;
        }
        if (!File.Exists(midiFilePath))
        {
            Debug.LogError("MIDI file not found at path: " + midiFilePath);
            yield break;
        }
        MidiFile midiFile = MidiFile.Read(midiFilePath);
        Debug.Log("Midi file loaded: " + midiFile.GetTrackChunks().Count() + " tracks");
        foreach (var chunk in midiFile.GetTrackChunks())
        {
            Debug.Log("Track: " + chunk.Events.Count() + " events");
        }

        _drumMidiNoteSpawnerScript.SpawnMidiNotes(midiFile);
        _drumMasterTimingScript.Play();
    }
}
