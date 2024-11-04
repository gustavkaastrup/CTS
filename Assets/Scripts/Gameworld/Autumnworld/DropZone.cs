using UnityEngine;

public class DropZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Überprüfen, ob der Fuchs in die Drop-Zone gezogen wurde
        if (other.gameObject.CompareTag("fox"))
        {
            Debug.Log("Der Fuchs ist im Haus!");
            // Hier kannst du Aktionen ausführen, wenn der Fuchs ins Haus gezogen wird
            // z.B. eine Animation starten, eine Nachricht anzeigen, etc.
        }
    }
}
