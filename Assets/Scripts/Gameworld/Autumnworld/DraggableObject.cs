using UnityEngine;

public class DraggableObject : MonoBehaviour

{
    private Vector2 offset = Vector2.zero;

    private void OnMouseDown()
    {
        // Berechne die Differenz zwischen der Mausposition und der Position des MushroomHouse
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = (Vector2)transform.position - mousePosition;
    }

    private void OnMouseDrag()
    {
        // Setze die neue Position des MushroomHouse basierend auf der Mausposition und der gespeicherten Differenz
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition + offset;
    }
}

