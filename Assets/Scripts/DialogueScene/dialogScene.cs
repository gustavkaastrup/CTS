using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button nextButton;
    public Button finishGameButton;
    [Range(0, 3)] public int gameWorldIndex;
    [Range(0, 2)] public int levelIndex;
    
    private Loader loader;

    private List<string>[,] introDialogues = new List<string>[4, 3];

    private List<string>[] outroLastGameworldLevelDialogue = new List<string>[4];

    private void InitDialogues()
    {
        introDialogues[0, 0] = new List<string>(){
            "I think Im going start the search for new bandmates in my own house.",
            "I can play bass guitar but you need someone who plays regular guitar - and my brother has been playin for years",
            "However he doesnt believe that our band can succeed :( I have to convince him by my excelent bass playing!",
            "But first I need to learn how do the guitar effects work, I remember my brother talking about them - lets try it!"
         };

        introDialogues[0, 1] = new List<string>()
        {
            "Ok, so I leard some basics, now its time practice a bit more, so that I dont embarace myself infront of my brother."
        };

        introDialogues[0, 2] = new List<string>()
        {
            "I just talked to my brother and so now its time to show him what Ive learned.",
            "However our parents are currently cleaning the kitchen, so its going to be a bit noisy, I will have to listen carefully.",
            "Hopefully he likes it and joins my band - than we can keep searching for other band members and finally perform at the big festival!",
        };

          introDialogues[1, 0] = new List<string>(){
            "You found us in the forest!",
            "We have to somehow wake up bear so he can join our band!",
            "Maybe if we play some guitar he will wake up! He loves Rock music so that is sure to wake him up", 
            "Come on play some rock music to wake him up! Lets try Eye of the Tiger that is one of is favorites",
            "For this game you have to play the notes as they hit the circles at the bottom. Each lane corresponds to a arrow on your keyboard."
         };

        introDialogues[1, 1] = new List<string>()
        {
            "Damn, that was not enough to wake him up.",
            "Let us try another song he loves I Love Rock 'n' Roll"
        };

        introDialogues[1, 2] = new List<string>()
        {
           "Wow, not even I Love Rock 'n' Roll was enough to wake him up.",
           "I know a song that is guaranteed to wake him up",
           "His favorite song of all time is Slow Ride",
           "You will have to play this song! However this is a pretty hard song and you will have to play it well for him to wake up"
        };

        introDialogues[2, 0] = new List<string>() { 
            "We need to find the next member of our band, the vocalist!",
            "We heard that the bird is the best singer on the whole island.",
            "We have to convince him to join our band!",
            "Unfortunately, he only listens to weird electronic music.",
            "You have to convince him to play rock and roll with us, by blasting his socks off on the drums!",
            "You play the drums by pressing K, S, and H on your keyboard. Go ahead and try it!",
            "You have to hit correct drum in time with the music. The better you play the more points you get!",
            "The Notes will light up when they're supposed to be played, and beware sometimes you have to play two notes at the same time!",
            "Good Luck!"

        };
        introDialogues[2, 1] = new List<string>() { 
            "That rocked! Unfortunately the bird isn't convinced yet, maybe we should try something faster",
            "I got just the thing..." };
        introDialogues[2, 2] = new List<string>() { 
            "That didn't work either... Maybe it was too slow?",
            "I know! Try playing something even faster!",
            "1,2,3,4, GO!" 
            };

        introDialogues[3, 0] = new List<string>() 
        {
            "This is our big chance! They will let us perform on festival if we prove that we are good enaugh.",
            "Because the main organizer is an old singer from legendary band Four Ferrets!",
            "He chanllanged Bird to a singing battle - if Bird wins, they will let us perform",
            "But first, Bird needs to warm up his vocal cords before he sings in front of the festival organizors.",
            "You can play the reference melody by pressing BIG white button",
            "After the melody is played you have to replay it by pressing Q, W and E on your keyboard (you can try it before pressing the button).",
            "However you have limited time, blue square indicates the timer. Are you ready?"
        };
        introDialogues[3, 1] = new List<string>() 
        {
            "Its time for Bird to sing in front of the organizors.",
            "The battle is going to be similar to warm up - first you are going to listen to a melody and than try to repeat it.",
            "However this time the melodies are going to be a bit longer..."
        };
        introDialogues[3, 2] = new List<string>()
        {
            "They liked the performance, but they want to see if Bird can sing a bit faster - good singer has to be able to sing FAST!",
            "So this time you are going to have less time to play the melody"
        };


        outroLastGameworldLevelDialogue[0] = new List<string>()
        {
            "My brother really liked it so he's gonna join my new band! I know about bear-drummer, he's living in the forest - let's go back to world selection and go to next world :)",
        };
        outroLastGameworldLevelDialogue[1] = new List<string>()
        {
            "outro 1",
        };
        outroLastGameworldLevelDialogue[2] = new List<string>()
        {
            "outro 2",
        };
        outroLastGameworldLevelDialogue[3] = new List<string>()
        {
            "Bird has won the singing battle!!! Now its finally time to perform on the big stage",
        };

    }

    private List<string> failLevelDialogue = new List<string>()
    {
        "You didnt have enough points, try again!"
    };
    private List<string> successlevelDialogue = new List<string>()
    {
        "Congrats on completing the level! You can go back and go to the next level."
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
                    dialogue = outroLastGameworldLevelDialogue[gameWorldIndex];
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

        if (loader.GetGameworldIndex() == 4)
        {
            if (finishGameButton != null)
            {
                finishGameButton.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Finish button is null");
            }
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
