using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchVideo : MonoBehaviour
{
    public float delay = 85;
    public string NewLevel= "Gameworld";
    void Start()
    {
        StartCoroutine(LoadLevelAfterDelay(delay));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(NewLevel);
    }
}