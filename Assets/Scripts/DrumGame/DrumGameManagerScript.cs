
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrumGameManagerScript : MonoBehaviour
{
    public GameObject KickDrum;
    public GameObject SnareDrum;
    public GameObject HiHat;
    public GameObject Ghost;
    public GameObject restartButton;
    public GameObject circle;
    public GameObject rotator;

    public GameObject rotatorSprite;

    private int counter = 0;
    private TMP_Text scoreText;
    private TMP_Text nameText;


    private string playerName;
    private string HighscoreName;
    public string Difficulty = "Easy";

    private int Highscore;
    public int Score = 0;
    public static DrumGameManagerScript instance;
    private ConductorScript conductor;
    private int KickRadius = 3;
    private int SnareRadius = 5;
    private int HiHatRadius = 7;
    public bool[][] KickPatterns;
    public bool[][] SnarePatterns;
    public bool[][] HiHatPatterns;

    private bool verse = false;
    private bool ghostSpawned = false;
    private bool chorus = false;
    public bool playing = false;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        conductor = ConductorScript.instance;
        scoreText = GameObject.FindWithTag("Score").GetComponent<TMP_Text>();
        nameText = GameObject.FindWithTag("Name").GetComponent<TMP_Text>();
        Highscore = PlayerPrefs.GetInt("Highscore");
        HighscoreName = PlayerPrefs.GetString("HighscoreName");
        SetDifficulty(Difficulty);
    }

    void Update()
    {
        if (playing)
        {
            if (ghostSpawned == false && conductor.songPosInBeats < 4)
            {
                SpawnGhosts();
                ghostSpawned = true;
                Debug.Log("ghosts spawned");
            }
            if (conductor.songPosInBeats > 4 && !verse)
            {
                Spawn();
                verse = true;
                DespawnGhosts();
                ghostSpawned = false;
            }
            if (conductor.songPosInBeats > 48 && ghostSpawned == false)
            {
                counter++;
                SpawnGhosts();
                ghostSpawned = true;
            }
            if (conductor.songPosInBeats > 52 && !chorus)
            {
                if (Difficulty == "Hard")
                {
                    rotatorSprite.GetComponent<SpriteRenderer>().enabled = false;
                }
                Despawn();
                chorus = true;
                DespawnGhosts();
                Spawn();
            }

            scoreText.text = "Highscore: " + HighscoreName + " " + Highscore + "\nScore: " + Score;
        }
    }

    public void SetDifficulty(string difficulty)
    {
        Difficulty = difficulty;
        switch (Difficulty)
        {
            case "Easy":
                KickPatterns = new bool[][] {
                    new bool[] { true, false, false, false, true, false, false, false },
                    new bool[] { true, false, true, false, true, false, true, false } };
                SnarePatterns = new bool[][] {
                    new bool[] { false, false, false, false, false, false, false, false },
                    new bool[] { false, false, true, false, false, false, true, false } };
                HiHatPatterns = new bool[][] {
                    new bool[] { false, false, false, false, false, false, false, false },
                    new bool[] { false, false, false, false, false, false, false,false,} };
                break;
            case "Medium":
                KickPatterns = new bool[][] {
                    new bool[] { true, false, true, false, true, false, true, false },
                    new bool[] { true, false, true, false, true, false, true, false }, };
                SnarePatterns = new bool[][] {
                    new bool[] { false, false, false, false, false, false, false, false },
                    new bool[] { false, false, true, false, false, false, true, false } };
                HiHatPatterns = new bool[][] {
                    new bool[] { false, false, false, false, false, false, false, false },
                    new bool[] { true, false, true, false, true, false, true, false } };
                break;
            case "Hard":
                KickPatterns = new bool[][] {
                    new bool[] { true, false, false, false, false, true, false, false },
                    new bool[] { true, false, false, false, false, true, false, false } };
                SnarePatterns = new bool[][] {
                    new bool[] { false, false, false, false, false, false, false, false },
                    new bool[] { false, false, true, false, false, false, true, false } };
                HiHatPatterns = new bool[][] {
                    new bool[] { true, false, true, false, true, false, true, false },
                    new bool[] { true, false, true, false, true, false, true, false } };
                break;
        }
    }

    public void Spawn()
    {
        Debug.Log(KickPatterns[counter].ToString());
        KickDrum.GetComponent<DrumSpawnerScript>().DrumPattern = KickPatterns[counter];
        SnareDrum.GetComponent<DrumSpawnerScript>().DrumPattern = SnarePatterns[counter];
        HiHat.GetComponent<DrumSpawnerScript>().DrumPattern = HiHatPatterns[counter];
        KickDrum.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(KickRadius);
        SnareDrum.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(SnareRadius);
        HiHat.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(HiHatRadius);
    }
    public void SpawnGhosts()
    {
        Ghost.GetComponent<DrumSpawnerScript>().DrumPattern = KickPatterns[counter];
        Ghost.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(KickRadius);
        Ghost.GetComponent<DrumSpawnerScript>().DrumPattern = SnarePatterns[counter];
        Ghost.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(SnareRadius);
        Ghost.GetComponent<DrumSpawnerScript>().DrumPattern = HiHatPatterns[counter];
        Ghost.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(HiHatRadius);
    }

    public void DespawnGhosts()
    {
        Ghost.GetComponent<DrumSpawnerScript>().DeleteDrums();
    }

    public void Despawn()
    {
        KickDrum.GetComponent<DrumSpawnerScript>().DeleteDrums();
        SnareDrum.GetComponent<DrumSpawnerScript>().DeleteDrums();
        HiHat.GetComponent<DrumSpawnerScript>().DeleteDrums();
    }

    public void AddScore(int score)
    {
        Score += score;
    }
    public void GameOver()
    {
        playing = false;
        Debug.Log("Game Over");
        if (KickDrum == null) Debug.LogError("KickDrum is null");
        if (SnareDrum == null) Debug.LogError("SnareDrum is null");
        if (HiHat == null) Debug.LogError("HiHat is null");
        if (circle == null) Debug.LogError("circle is null");
        if (rotator == null) Debug.LogError("rotate is null");
        if (scoreText == null) Debug.LogError("scoreText is null");
        if (restartButton == null) Debug.LogError("restartButton is null");
        KickDrum.GetComponent<DrumSpawnerScript>().DeleteDrums();
        SnareDrum.GetComponent<DrumSpawnerScript>().DeleteDrums();
        HiHat.GetComponent<DrumSpawnerScript>().DeleteDrums();
        if (Score > Highscore)
        {
            Highscore = Score;
            PlayerPrefs.SetString("HighscoreName", playerName);
            PlayerPrefs.SetInt("Highscore", Highscore);
        }
        scoreText.rectTransform.anchoredPosition = new Vector2(0, 0);
        scoreText.text = "GAME OVER\nFinal Score: " + Score;
        Debug.Log("text should change");
        circle.gameObject.SetActive(false);
        rotator.gameObject.SetActive(false);
        restartButton.SetActive(true);
    }

    public void StartGame()
    {
        conductor.StartSong();
        playing = true;
        playerName = nameText.text;
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.SetString("HighscoreName", "");
        Highscore = 0;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
