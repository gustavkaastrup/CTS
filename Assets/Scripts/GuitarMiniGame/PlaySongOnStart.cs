using UnityEngine;

public class PLaySongOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip startMusicClip;

    private void Start()
    {
        if (startMusicClip != null && AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(startMusicClip);
        }
    }
}