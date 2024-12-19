using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button nextButton;
    [Range(0, 3)] public int gameWorldIndex;
    [Range(0, 2)] public int levelIndex;
    
    private Loader loader;

    private List<string>[,] introDialogues = new List<string>[4, 3];

    private void InitDialogues()
    {
        introDialogues[0, 0] = new List<string>(){
            "Hi, you found the right spot to convince my brother to join the band!",
            "You gotta nail this minigame by singing the correct melody. If you can pull that off, we’ll keep searching for band members so we can perform at the big festival.",
            "You can play the reference melody by pressing BIG white button", 
            "After the melody is played you have to replay it by pressing Q, W and E on your keyboard (you can try it before pressing the button).",
            "However you have limited time, blue square indicates the timer. Are you ready?"
         };

        introDialogues[0, 1] = new List<string>()
        {
            "Now its gonna get a bit harder, there are going to be more notes in one melody"
        };

        introDialogues[0, 2] = new List<string>()
        {
            "This time you are going to have less time - good singer has to be able to sing FAST!"
        };

        introDialogues[1, 0] = new List<string>() { "1 0 intro" };
        introDialogues[1, 1] = new List<string>() { "1 1 intro" };
        introDialogues[1, 2] = new List<string>() { "1 2 intro" };

        introDialogues[2, 0] = new List<string>() { "2 0 intro" };
        introDialogues[2, 1] = new List<string>() { "2 1 intro" };
        introDialogues[2, 2] = new List<string>() { "2 2 intro" };

        introDialogues[3, 0] = new List<string>() { "3 0 intro" };
        introDialogues[3, 1] = new List<string>() { "3 1 intro" };
        introDialogues[3, 2] = new List<string>() { "3 2 intro" };
    }

    private List<string> failLevelDialogue = new List<string>()
    {
        "You didnt have enough points, try again!"
    };
    private List<string> successlevelDialogue = new List<string>()
    {
        "Congrats on completing the level! You can go back and go to the next level."
    };
    private List<string> successLastGameworldLevelDialogue = new List<string>()
    {
        "Congrats on completing last level of this world! Go back to world selection and go to next world :)",
    };
    

    private int currentDialogueIndex = 0;
    private List<string> dialogue;
    private void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
        Loader.LevelState levelState = loader.levelsState[gameWorldIndex][levelIndex];
        InitDialogues();

        switch (levelState)
        {
            case Loader.LevelState.NotPlayed:
                dialogue = introDialogues[gameWorldIndex, levelIndex];
                break;
            case Loader.LevelState.Finished:
                if (levelIndex == 2)
                {
                    if (gameWorldIndex == 3)
                    {
                        // Finished last level of game
                        loader.LoadScene(Loader.Scene.GameworldLastScene);
                        return;
                    }
                    dialogue = successLastGameworldLevelDialogue;
                }
                else
                {
                    dialogue = successlevelDialogue;
                }
                nextButton.gameObject.SetActive(false);
                break;
            case Loader.LevelState.Failed:
                dialogue = failLevelDialogue;
                break;
        }

        ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogue.Count)
        {
            dialogueText.text = dialogue[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            loader.LoadLevelScene(gameWorldIndex, levelIndex);
        }
    }
}
