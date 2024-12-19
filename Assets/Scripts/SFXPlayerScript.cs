using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayerScript : MonoBehaviour
{
    public static SFXPlayerScript instance;
    public AudioSource audioSource;
    public AudioClip buttonPressClip;
    public AudioClip nextLevelClip;
    public AudioClip gameOverClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButtonPress()
    {
        audioSource.PlayOneShot(buttonPressClip);
    }

    public void PlayNextLevel()
    {
        audioSource.PlayOneShot(nextLevelClip);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverClip);
    }
}
