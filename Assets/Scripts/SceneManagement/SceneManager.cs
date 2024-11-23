using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static Loader Instance;
    public enum Scene
    {
        Gameworld,
        Gameworld_Mushroom,
        Gameworld_Bar,
        Gameworld_Forest,
        Gameworld_Stage
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(Scene sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

    public void ReloadCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
