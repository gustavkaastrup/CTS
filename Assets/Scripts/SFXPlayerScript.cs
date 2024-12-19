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
            DontDestroyOnLoad(gameObject); // Ensure the instance persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButtonPress()
    {
        if (buttonPressClip != null)
        {
            audioSource.PlayOneShot(buttonPressClip);
        }
        else
        {
            Debug.LogWarning("ButtonPressClip is not assigned.");
        }
    }

    public void PlayNextLevel()
    {
        if (nextLevelClip != null)
        {
            audioSource.PlayOneShot(nextLevelClip);
        }
        else
        {
            Debug.LogWarning("NextLevelClip is not assigned.");
        }
    }

    public void PlayGameOver()
    {
        if (gameOverClip != null)
        {
            audioSource.PlayOneShot(gameOverClip);
        }
        else
        {
            Debug.LogWarning("GameOverClip is not assigned.");
        }
    }
}