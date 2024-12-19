using UnityEngine;

public class DrumPlayerSoundManager : MonoBehaviour
{
    public static DrumPlayerSoundManager instance;

    public AudioClip kickSound;
    public AudioClip snareSound;
    public AudioClip hihatSound;
    public AudioClip missSound;
    private AudioSource audioSource;

    private void Awake()
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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string noteName)
    {
        switch (noteName)
        {
            case "C":
                audioSource.PlayOneShot(kickSound);
                break;
            case "D":
                audioSource.PlayOneShot(snareSound);
                break;
            case "E":
                audioSource.PlayOneShot(hihatSound);
                break;
        }
    }

    public void PlayMissSound()
    {
        audioSource.PlayOneShot(missSound);
    }
}