using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source ------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------ Audio Clip ------")]
    [Header("Bass minigame")]
    public AudioClip BassLowE;
    public AudioClip BassLowA;
    public AudioClip BassMidE;
    public AudioClip BassHighE;
    public AudioClip KitchenNoise;
    public AudioClip WashingNoise;
    public AudioClip KitchenWashingNoise;

    [Header("Vocals minigame")]
    public AudioClip VocalsC;
    public AudioClip VocalsE;
    public AudioClip VocalsG;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource.clip != musicClip)
        {
            musicSource.clip = musicClip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
            musicSource.clip = null;
        }
    }

    public void PlaySoundEffect(AudioClip soundClip)
    {
        SFXSource.PlayOneShot(soundClip);
    }

    public void PlaySoundEffectStopPrevious(AudioClip soundClip)
    {
        SFXSource.Stop();
        SFXSource.PlayOneShot(soundClip);
    }

    public IEnumerator PlayAudioInSequence(List<AudioClip> audioClipList)
    {
        foreach (AudioClip audioClip in audioClipList)
        {
            SFXSource.PlayOneShot(audioClip);
            yield return new WaitForSeconds(audioClip.length);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
