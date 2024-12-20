using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXPlayerScript : MonoBehaviour
{
    public static SFXPlayerScript instance;
    private AudioSource audioSource;
    public AudioClip buttonPressClip;
    public AudioClip nextLevelClip;
    public AudioClip gameOverClip;

   void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(gameObject); // Ensure the instance persists across scenes
            Debug.Log("SFXPlayerScript instance created.");
        }
        else
        {
            Debug.Log("SFXPlayerScript instance already exists. Destroying new instance.");
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        if (instance == this)
        {
            Debug.Log("SFXPlayerScript instance destroyed.");
        }
    }
    void Update(){
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlayButtonPress()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not initialized.");
            return;
        }

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
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not initialized.");
            return;
        }

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
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not initialized.");
            return;
        }

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