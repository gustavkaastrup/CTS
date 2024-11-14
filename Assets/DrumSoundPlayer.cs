using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSoundPlayer : MonoBehaviour
{
    public AudioSource kickSound;
    public AudioSource snareSound;
    public AudioSource hiHatSound;

    public KeyCode kickKey;
    public KeyCode snareKey;
    public KeyCode hiHatKey;

    // Start is called before the first frame update
    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(kickKey))
        {
            kickSound.Play();
        }
        if (Input.GetKeyDown(snareKey))
        {
            snareSound.Play();
        }
        if (Input.GetKeyDown(hiHatKey))
        {
            hiHatSound.Play();
        }
    }
}
