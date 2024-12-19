using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBarRotationScript : MonoBehaviour
{
    // Start is called before the first frame update
    AudioMidiController audioMidiController;
    private float barLength;
    private float bpm;

    void Start()
    {
        audioMidiController = AudioMidiController.instance;
        barLength = audioMidiController.GetBarLength();
        bpm = audioMidiController.bpm;
    }
    void Update()
    {
        float currentPosInBar = audioMidiController.currentPosInBar;
        float angle = currentPosInBar * 360;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
