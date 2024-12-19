using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayerScript : MonoBehaviour
{
    public static BGMPlayerScript instance;
    public AudioSource audioSource;
    public AudioClip barMusicClip;
    public AudioClip forestMusicClip;
    public AudioClip mushroomMusicClip;
    public AudioClip stageMusicClip;
    public enum MusicType
    {
        Bar,
        Forest,
        Mushroom,
        Stage
    }

    public MusicType currentMusicType;
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
    void Start()
    {
        switch (currentMusicType)
        {
            case MusicType.Bar:
                PlayBarMusic();
                break;
            case MusicType.Forest:
                PlayForestMusic();
                break;
            case MusicType.Mushroom:
                PlayMushroomMusic();
                break;
            case MusicType.Stage:
                PlayerStageMusic();
                break;
        }
    }
    public void PlayBarMusic(){
        if (barMusicClip != null)
        {
            audioSource.clip = barMusicClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("BarMusicClip is not assigned.");
        }
    }
    public void PlayForestMusic(){
        if(forestMusicClip != null){
            audioSource.clip = forestMusicClip;
            audioSource.Play();
        }
        else{
            Debug.LogWarning("ForestMusicClip is not assigned.");
        }
    }
    public void PlayMushroomMusic(){
        if(mushroomMusicClip != null){
            audioSource.clip = mushroomMusicClip;
            audioSource.Play();
        }
        else{
            Debug.LogWarning("MushroomMusicClip is not assigned.");
        }
    }
    public void PlayerStageMusic(){
        if(stageMusicClip != null){
            audioSource.clip = stageMusicClip;
            audioSource.Play();
        }
        else{
            Debug.LogWarning("StageMusicClip is not assigned.");
        }
    }
}
