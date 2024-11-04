using UnityEngine;
using UnityEngine.SceneManagement;

public class MushroomHouse : MonoBehaviour
{
    // Diese Methode wird aufgerufen, wenn das Objekt angeklickt wird
    void OnMouseDown()
    {
        // Wechselt zur Dialogszene
        SceneManager.LoadScene("DialogScene");
    }
}
