using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSetScript : MonoBehaviour
{
    public AudioClip kickSound;
    public AudioClip snareSound;
    public AudioClip hiHatSound;
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            audioSource.PlayOneShot(kickSound);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            audioSource.PlayOneShot(snareSound);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            audioSource.PlayOneShot(hiHatSound);
        }
    }
}
