using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VocalsTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill;

    private float Duration;
    private float remainingDuration;
    private float secondsPerNote;
    private Action actionOnEnd;
    private bool isRunning;

    public void Begin(int noteCount, float secondsPerNote, Action actionOnEnd)
    {
        Duration = noteCount * secondsPerNote;
        remainingDuration = Duration;
        this.secondsPerNote = secondsPerNote;
        this.actionOnEnd = actionOnEnd;
        isRunning = true;
        StartCoroutine(UpdateTimer());
    }

    public void Stop()
    {
        isRunning = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (isRunning && remainingDuration >= 0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration-=secondsPerNote;
            yield return new WaitForSeconds(secondsPerNote);
        }
        if (isRunning)
        {
            OnEnd();
        }
    }

    private void OnEnd()
    {
        if (actionOnEnd != null)
        {
            actionOnEnd();
        } else
        {
            Debug.Log("Vocals timer action on end not provided");
        }
    }
}
