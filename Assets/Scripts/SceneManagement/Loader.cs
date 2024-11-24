using System;
using System.Collections.Generic;
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
        Gameworld_Stage,

        DialogueSceneEntrance_1,
        DialogueSceneKitchen_2,
        DialogueSceneLivingroom_3
    }

    private Dictionary<Scene, bool> isSceneAviable;
    private HashSet<Scene> initialAviableScenes = new HashSet<Scene> { 
        Scene.Gameworld,
        Scene.Gameworld_Mushroom,
        Scene.DialogueSceneEntrance_1
    };
    private Stack<Scene> lastSenesStack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            isSceneAviable = new Dictionary<Scene, bool>();
            foreach (Scene scene in Enum.GetValues(typeof(Scene)))
            {
                isSceneAviable[scene] = initialAviableScenes.Contains(scene);
            }
            lastSenesStack = new Stack<Scene>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(Scene sceneName)
    {
        if (isSceneAviable[sceneName])
        {
            String currentSceneName = SceneManager.GetActiveScene().name;
            if (!Enum.TryParse(currentSceneName, out Scene currentScene))
            {
                Debug.Log("Loader: current scene not in Loader.Scene");
            }
            else
            {
                lastSenesStack.Push(currentScene);
            }
            SceneManager.LoadScene(sceneName.ToString());
            
        } else
        {
            Debug.Log("Loader: " + sceneName.ToString() + " scene is not aviable");
        }
    }

    public void ReloadCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void MakeSceneAviable(Scene scene)
    {
        isSceneAviable[scene] = true;
    }

    public void LoadLastScene()
    {
        Scene lastScene = lastSenesStack.Pop();
        SceneManager.LoadScene(lastScene.ToString());
    }
}
