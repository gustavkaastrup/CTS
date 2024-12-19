
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BGMPlayerScript : MonoBehaviour
{
    public static BGMPlayerScript instance;
    public AudioSource audioSource;
    public AudioClip barMusicClip;
    public AudioClip forestMusicClip;
    public AudioClip mushroomMusicClip;
    public AudioClip stageMusicClip;
    public AudioClip mainMenuMusicClip;

    private string currentScene;
    public bool dontPlay;
    public enum MusicType
    {
        Bar,
        Forest,
        Mushroom,
        Stage,
        MainMenu
    }

    void Awake()
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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != currentScene)
        {
            currentScene = SceneManager.GetActiveScene().name;
            if(currentScene.Contains("Dialogue")){
                return;
            }
            switch (SceneManager.GetActiveScene().name)
            {
            case "Gameworld_Bar":
                currentMusicType = MusicType.Bar;
                dontPlay = false;
                break;
            case "Gameworld_Forest":
                currentMusicType = MusicType.Forest;
                dontPlay = false;
                break;
            case "Gameworld_Mushroom":
                currentMusicType = MusicType.Mushroom;
                dontPlay = false;
                break;
            case "Gameworld_Stage":
                currentMusicType = MusicType.Stage;
                dontPlay = false;
                break;
            case "Gameworld" or "MainMenu":
                currentMusicType = MusicType.MainMenu;
                dontPlay = false;
                break;
            default:
                audioSource.Stop();
                dontPlay = true;
                break;
            }
            
            if (!dontPlay)
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
                    case MusicType.MainMenu:
                        PlayMainMenuMusic();
                        break;
                }
            }
        }
    }

    public MusicType currentMusicType;
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
    public void PlayMainMenuMusic(){
        if(mainMenuMusicClip != null){
            audioSource.clip = mainMenuMusicClip;
            audioSource.Play();
        }
        else{
            Debug.LogWarning("MainMenuMusicClip is not assigned.");
        }
    }
}
