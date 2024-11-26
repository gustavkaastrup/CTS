using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingCircleScript : MonoBehaviour
{
    private int _targetSample;
    private DrumMasterTimingScript _timingScript;
    private bool _isHit = false;
    public float noteNumber;
    public float hitWindowSeconds = 0.2f;
    public float targetRadius = 2f;
    private SpriteRenderer _sprite;

    public void Initialize(int targetSample, DrumMasterTimingScript timingScript, int note)
    {
        _targetSample = targetSample;
        _timingScript = timingScript;
        noteNumber = note;
        transform.localScale = Vector3.zero;
    }
    void Update()
    {
        int currentSample = _timingScript.CurrentSample;
        float progress = Mathf.Clamp01((float)(currentSample - (_targetSample - _timingScript.sampleRate)) / _timingScript.sampleRate);
        transform.localScale = Vector3.one * progress * targetRadius;
        if (Mathf.Abs(currentSample - _targetSample) <= hitWindowSeconds * _timingScript.sampleRate)
        {
            _sprite.color = Color.green;
            print(transform.localScale.x);
        }
        else
        {
            _sprite.color = Color.red;
        }
        if (!_isHit && currentSample > _targetSample + hitWindowSeconds + _timingScript.sampleRate)
        {
            MissNote();
        }
        if (progress >= 1)
        {
            MissNote();
        }
    }

    void Start()
    {
        DrumPlayerInputManagerScript.instance.RegisterNote(this);
        _sprite = GetComponent<SpriteRenderer>();
    }

    void OnDestroy()
    {
        DrumPlayerInputManagerScript.instance.UnregisterNote(this);
    }

    void OnKeyPress()
    {
        int currentSample = _timingScript.CurrentSample;
        if (Mathf.Abs(currentSample - _targetSample) <= hitWindowSeconds * _timingScript.sampleRate)
        {
            HitNote();
        }
    }

    public void TryHit(KeyCode key)
    {
        if (DrumNoteMapperScript.instance.KeyToNote.TryGetValue(key, out int note))
        {
            if (note == noteNumber)
            {
                OnKeyPress();
            }
        }
    }

    void MissNote()
    {
        Debug.Log("Missed Note: " + noteNumber);
        Destroy(gameObject);
    }
    void HitNote()
    {
        _isHit = true;
        Debug.Log("Hit Note: " + noteNumber);
        Destroy(gameObject);
    }
}
