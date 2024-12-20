using UnityEngine;

public class BarAmbience : MonoBehaviour
{
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;

    AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.SetMusicVolume(musicVolume);
        audioManager.PlayMusic(audioManager.KitchenWashingNoise);
    }
    private void OnDestroy()
    {
        audioManager.StopMusic();
    }
}
