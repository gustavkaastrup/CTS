using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using Unity.VisualScripting;

public class AudioMidiController : MonoBehaviour
{
    public static AudioMidiController instance;
    public string midiFilePath;
    public IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes;
    public TempoMap tempoMap;
    public AudioSource audioSource;
    private List<MidiNote> midiNotes = new List<MidiNote>();
    public float currentTime;
    public int sampleRate;
    public float currentPosInBar;
    public int currentBar;
    public int bpm;
    private float barLength;
    public int currentSample;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        MidiFile midiFile = MidiFile.Read(midiFilePath);
        notes = midiFile.GetNotes();
        audioSource = GetComponent<AudioSource>();
        tempoMap = midiFile.GetTempoMap();
        sampleRate = audioSource.clip.frequency;
        foreach (var note in notes)
        {
            var metricTimeSpan = note.TimeAs<MetricTimeSpan>(tempoMap);
            midiNotes.Add(new MidiNote
            {
                NoteName = note.NoteName.ToString(),
                Time =  metricTimeSpan.TotalSeconds
            });
        }
        Debug.Log($"Loaded {midiNotes.Count} MIDI notes.");
        Play();
    }

    void Update()
    {
        currentSample = audioSource.timeSamples;
        currentTime = (float)audioSource.timeSamples / sampleRate;
        currentBar = (int)(currentTime / GetBarLength());
        currentPosInBar = (currentTime % GetBarLength()) / GetBarLength();
    }

    public void Play()
    {
        sampleRate = audioSource.clip.frequency;
        audioSource.Play();
    }

    public float GetBarLength()
    {
         float bpm = this.bpm; // Retrieve BPM from the audio controller
         int beatsPerBar = 4;  // Adjust this for other time signatures
         return 60f / bpm * beatsPerBar;
    }

    public List<MidiNote> GetMidiNotes()
    {
        return midiNotes;
    }

    public float GetBeatLength()
    {
        return 60f / bpm;
    }
}

public class MidiNote
{
    public string NoteName { get; set; }
    public double Time { get; set; }
    public int Sample { get; set; }
}