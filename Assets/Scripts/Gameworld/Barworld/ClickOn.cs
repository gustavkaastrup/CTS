using UnityEngine;
using UnityEngine.SceneManagement;

public class MushroomHouse : MonoBehaviour
{
    // This method is called when the object is clicked
    void OnMouseDown()
    {
        // Load scenes based on the object's name
        if (gameObject.name == "MushroomHouse")
        {
            SceneManager.LoadScene("DialogueSceneKitchen"); // Kitchen scene
        }
        else if (gameObject.name == "MushroomHouse2")
        {
            SceneManager.LoadScene("DialogueSceneLivingroom"); // Living room scene
        }
        else if (gameObject.name == "MushroomHouse3")
        {
            SceneManager.LoadScene("DialogueSceneEntrance"); // Entrance scene
        }
        else
        {
            Debug.LogWarning("Unknown MushroomHouse object was clicked.");
        }
    }
}
