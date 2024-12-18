using System.Collections;
using System.Collections.Generic;
using TMPro;

//using System.Threading.Tasks;
using UnityEngine;

public class VocalGameLogic : MonoBehaviour
{
    public static AudioManager audioManager;
    public VocalsNoteIndicator noteIndicator;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI scoreText;
    public VocalsTimer timer;
    [Range(0f, 3f)]
    public float SECONDS_PER_NOTE = 1;
    [SerializeField]
    private int minGoalCorrectNotes;

    public enum Note
    {
        C,
        E,
        G
    }
    [System.Serializable]
    public class Melody
    {
        public List<Note> notes;
    }

    public List<Melody> melodies;
    public List<VocalsNoteIndicator> vocalsNoteIndicators;

    private int melodyIndex = 0;
    private int noteIndex = 0;
    private bool isRunning = true;
    private List<List<AudioClip>> melodiesAudioClips;
    private bool referenceMelodyPlayed;
    private bool referenceMelodyCurrentlyPlaying;

    private int correctNotesCount;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        InitiateMelodiesAudioClips();
        referenceMelodyPlayed = false;
        referenceMelodyCurrentlyPlaying = false;
        correctNotesCount = 0;
    }

    public void PlayNote(Note note)
    {
        if (referenceMelodyCurrentlyPlaying)
        {
            return;
        }

        AudioClip clip = GetAudioClipFromNote(note);
        if (clip != null)
        {
            audioManager.PlaySoundEffectStopPrevious(clip);
        }

        if (referenceMelodyPlayed)
        {
            bool playedCorrectNote = noteIndicator.PlayedNote(note, noteIndex);
            if (playedCorrectNote)
            {
                IncreaseCorrectNoteCount();
            }

            noteIndex++;
            if (noteIndex >= melodies[melodyIndex].notes.Count)     // last note of current melody was played
            {
                SetNewMelody();
            }
        }
    }
    
    private void SetNewMelody()
    {
        noteIndex = 0;
        melodyIndex++;
        if (melodyIndex >= melodies.Count)                   // last melody was played
        {
            isRunning = false;
        }
        else
        {
            noteIndicator.ResetNoteStoplights();
            referenceMelodyPlayed = false;
        }
    }
    private void IncreaseCorrectNoteCount()
    {
        correctNotesCount++;
        scoreText.text = correctNotesCount.ToString();
        //Debug.Log("vocals - wrong note count: " + wrongNotesCount);
    }

    private void InitiateMelodiesAudioClips()
    {
        melodiesAudioClips = new List<List<AudioClip>>();

        foreach (Melody melody in melodies)
        {
            List<AudioClip> melodyAudioClips = new List<AudioClip>();

            foreach (Note note in melody.notes)
            {
                AudioClip audioClip = GetAudioClipFromNote(note);
                if (audioClip != null)
                {
                    melodyAudioClips.Add(audioClip);
                }
            }
            melodiesAudioClips.Add(melodyAudioClips);
        }
    }

    public void PlayCurrentMelody()
    {
        if (!referenceMelodyPlayed)
        {
            List<AudioClip> melodyAudioClips = melodiesAudioClips[melodyIndex];
            StartCoroutine(PlayMelody(melodyAudioClips));
            referenceMelodyPlayed = true;
        }
    }


    private IEnumerator PlayMelody(List<AudioClip> melodyAudioClips)
    {
        referenceMelodyCurrentlyPlaying = true;
        instructionText.text = "LISTEN";
        yield return StartCoroutine(audioManager.PlayAudioInSequence(melodyAudioClips));
        instructionText.text = "PLAY";
        timer.Begin(melodyAudioClips.Count, SECONDS_PER_NOTE, OnTimerEnd);
        referenceMelodyCurrentlyPlaying = false;
    }

    private void OnTimerEnd()
    {
        //Debug.Log("LOGIC: timer ended");
        SetNewMelody();
    }

    public static AudioClip GetAudioClipFromNote(Note note)
    {
        switch (note)
        {
            case Note.C:
                return audioManager.VocalsC;
            case Note.E:
                return audioManager.VocalsE;
            case Note.G:
                return audioManager.VocalsG;
            default:
                Debug.Log("Vocals: Note to audio clip returned null");
                return null;
        }
    }

    private void EndLevel()
    {
        if (correctNotesCount >= minGoalCorrectNotes)
        {

        }
    }
    public Melody GetCurrentMelody()
    {
        return melodies[melodyIndex];
    }
}
