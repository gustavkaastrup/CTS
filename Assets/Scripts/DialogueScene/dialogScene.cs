using TMPro; // Stelle sicher, dass du das TextMesh Pro Namespace importierst
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText; // Referenz auf das TMP_Text-Element
    public Loader.Scene minigameLevelScene;

    private Loader loader;

    private string[] dialogues = {
        "Hi, you found the right spot to convince my brother to join the band!",
        "You gotta nail this minigame by catching the right notes. If you can pull that off, we’ll keep searching for band members so we can perform at the big festival.",
        "You can catch the notes using the keys D, F, G, H, J. Are you ready?"
    };

    private int currentDialogueIndex = 0;

    private void Start()
    {
        ShowNextDialogue(); // Zeigt den ersten Dialog an
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
    }

    public void UpdateDialogue(string newText)
    {
        dialogueText.text = newText; // Aktualisiert den Text
    }

    public void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            UpdateDialogue(dialogues[currentDialogueIndex]);
            currentDialogueIndex++;
        }
        else
        {
            loader.LoadScene(minigameLevelScene);
        }
    }
}
