using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [HideInInspector]
    public bool lastLevelSuccess;
    [HideInInspector]
    public bool levelPlayed = false;

    public static Loader Instance;
    public enum Scene
    {
        Gameworld,
        Gameworld_Mushroom,
        Gameworld_Bar,
        Gameworld_Forest,
        Gameworld_Stage,
        GameworldLastScene,

        DialogueSceneMushroom_1,
        DialogueSceneMushroom_2,
        DialogueSceneMushroom_3,

        DialogueSceneForest_1,
        DialogueSceneForest_2,
        DialogueSceneForest_3,

        BassLevel_1,
        BassLevel_2,
        BassLevel_3,

        VocalsLevel_1,
        VocalsLevel_2,
        VocalsLevel_3,
    }

    private HashSet<Scene> aviableScenes = new HashSet<Scene> {
        Scene.Gameworld,
        Scene.Gameworld_Mushroom,
        Scene.DialogueSceneMushroom_1,

        Scene.VocalsLevel_1,
        Scene.VocalsLevel_2,
        Scene.VocalsLevel_3,

        Scene.BassLevel_1,
        Scene.BassLevel_2,
        Scene.BassLevel_3,
    };

    private int levelIndex = 0;
    private int gameworldIndex = 0;
    private List<List<Scene>> dialogScenes = new List<List<Scene>>()
    {
        new List<Scene>() {Scene.DialogueSceneMushroom_1, Scene.DialogueSceneMushroom_2, Scene.DialogueSceneMushroom_3},
        new List<Scene>() {Scene.DialogueSceneForest_1, Scene.DialogueSceneForest_2, Scene.DialogueSceneForest_3},
    };
    private List<Scene> gameworldScenes = new List<Scene>()
    {
        Scene.Gameworld_Mushroom, Scene.Gameworld_Forest, Scene.Gameworld_Bar, Scene.Gameworld_Stage
    };
    private List<List<Scene>> levelScenes = new List<List<Scene>>()
    {
        new List<Scene>() {Scene.VocalsLevel_1, Scene.VocalsLevel_2, Scene.VocalsLevel_3},
        new List<Scene>() {Scene.BassLevel_1, Scene.BassLevel_2, Scene.BassLevel_3},
        new List<Scene>() {Scene.VocalsLevel_1, Scene.VocalsLevel_2, Scene.VocalsLevel_3},
        new List<Scene>() {Scene.VocalsLevel_1, Scene.VocalsLevel_2, Scene.VocalsLevel_3},
    };

    private Stack<Scene> lastScenesStack;

    public enum LevelState
    {
        NotPlayed,
        Finished,
        Failed
    }
    public List<List<LevelState>> levelsState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            lastScenesStack = new Stack<Scene>();
            InitFinishedLevels();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitFinishedLevels()
    {
        levelsState = new List<List<LevelState>>();
        for (int i = 0; i < 4; i++)
        {
            levelsState.Add(new List<LevelState>());
            for (int j = 0; j < 3; j++)
            {
                levelsState[i].Add(LevelState.NotPlayed);
            }
        }
    }

    public void LoadScene(Scene sceneName)
    {
        if (aviableScenes.Contains(sceneName))
        {
            String currentSceneName = SceneManager.GetActiveScene().name;
            if (!Enum.TryParse(currentSceneName, out Scene currentScene))
            {
                Debug.Log("Loader: current scene not in Loader.Scene");
            }
            else
            {
                lastScenesStack.Push(currentScene);
            }
            SceneManager.LoadScene(sceneName.ToString());
            
        } else
        {
            Debug.Log("Loader: " + sceneName.ToString() + " scene is not aviable yet");
        }
    }

    public void LoadLevelScene(int gameworldIndex,  int levelIndex)
    {
        LoadScene(levelScenes[gameworldIndex][levelIndex]);
    }

    public void ReloadCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void LevelSuccess()
    {
        levelsState[gameworldIndex][levelIndex] = LevelState.Finished;
        lastLevelSuccess = true;
        levelPlayed = false;
        levelIndex++;
        if (levelIndex == 3)                                    // last level of gameworld complete
        {
            levelIndex = 0;
            gameworldIndex++;
            if (gameworldIndex == 4)                            // last gameworld complete
            {
                MakeSceneAviable(Scene.GameworldLastScene);
                LoadScene(Scene.GameworldLastScene);
                return;
            }
            else
            {
                MakeSceneAviable(gameworldScenes[gameworldIndex]);
            }
        } 

        Scene nextDialogeScene = dialogScenes[gameworldIndex][levelIndex];
        MakeSceneAviable(nextDialogeScene);
        LoadLastScene();
    }
    public void LevelFailed()
    {
        levelsState[gameworldIndex][levelIndex] = LevelState.Failed;
        lastLevelSuccess = false;
        levelPlayed = true;
        LoadLastScene();
    }

    public void MakeSceneAviable(Scene scene)
    {
        aviableScenes.Add(scene);
    }

    public void LoadLastScene()
    {
        Scene lastScene = lastScenesStack.Pop();
        SceneManager.LoadScene(lastScene.ToString());
    }
}
