using TMPro; // Stelle sicher, dass du das TextMesh Pro Namespace importierst
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText; // Referenz auf das TMP_Text-Element
    public Loader.Scene minigameLevelScene;
    public Button nextButton;
    [Range(0, 3)] public int gameWorldIndex;
    [Range(0, 2)] public int levelIndex;
    
    private Loader loader;

    private string[] introDialogues = {
        "Hi, you found the right spot to convince my brother to join the band!",
        "You gotta nail this minigame by playing the correct melody. If you can pull that off, we’ll keep searching for band members so we can perform at the big festival.",
        "You can play the reference melody by pressing BIG white button - after the melody is played you have to replay it by pressing Q, W and E on your keyboard (you can try it before pressing the button).",
        "However you have limited time, blue square indicates the timer. Are you ready?"
    };
    private string[] failLevelDialogue =
    {
        "You didnt have enough point, try again!"
    };
    private string[] successlevelDialogue =
    {
        "Congrats on completing the level! You can go back and go to the next level."
    };

    private int currentDialogueIndex = 0;
    private string[] dialogues;
    private void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
        Loader.LevelState levelState = loader.levelsState[gameWorldIndex][levelIndex];

        if (!loader.levelPlayed)
        {
            dialogues = introDialogues;
        }
        else if (loader.lastLevelSuccess)
        {
            dialogues = successlevelDialogue;
        }
        else
        {
            dialogues = failLevelDialogue;
        }
        ShowNextDialogue(); // Zeigt den ersten Dialog an
    }

    public void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            loader.LoadScene(minigameLevelScene);
        }
    }
}
