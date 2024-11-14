
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System.Linq;



public class DrumGameManagerScript : MonoBehaviour
{
    enum SongParts
    {
        Intro,
        Verse,
        Chorus,
        Outro
    }
    public static DrumGameManagerScript instance;
    public GameObject KickDrum;
    public GameObject SnareDrum;
    public GameObject HiHat;
    public GameObject KickGhost;
    public GameObject SnareGhost;
    public GameObject HiHatGhost;
    public GameObject RestartButton;
    public GameObject Circle;
    public GameObject Rotator;
    public TMP_Text CountdownText;
    public string Difficulty = "Easy";
    public bool[][] EasyKickPatterns;
    public bool[][] EasySnarePatterns;
    public bool[][] EasyHiHatPatterns;
    public bool[][] MediumSnarePatterns;
    public bool[][] MediumKickPatterns;
    public bool[][] MediumHiHatPatterns;
    public bool[][] HardKickPatterns;
    public bool[][] HardSnarePatterns;
    public bool[][] HardHiHatPatterns;
    public int[] SongStructure;
    public int[] SongChanges;
    public bool Playing = false;
    public int Score = 0;

    private TMP_Text scoreText;
    private TMP_Text nameText;
    private ConductorScript conductor;
    private string playerName;
    private string highscoreName;
    private int highscore;
    private int kickRadius = 3;
    private int snareRadius = 5;
    private int hiHatRadius = 7;
    public int currentPart = 0;
    private bool[][] KickPatterns;
    private bool[][] SnarePatterns;
    private bool[][] HiHatPatterns;
    private bool[] SongFunction;
    private bool[] GhostFunction;
    private int currentBeat;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SongFunction = new bool[SongChanges.Max() + 1];
        GhostFunction = new bool[SongFunction.Length];
        for (int i = 0; i < SongChanges.Length; i++)
        {
            SongFunction[SongChanges[i]] = true;
            if (i > 0)
            {
                GhostFunction[SongChanges[i] - 1] = true;
            }
        }
        Playing = false;
        conductor = ConductorScript.instance;
        scoreText = GameObject.FindWithTag("Score").GetComponent<TMP_Text>();
        nameText = GameObject.FindWithTag("Name").GetComponent<TMP_Text>();
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreName = PlayerPrefs.GetString("HighscoreName");
        Difficulty = "Easy";
        CountdownText.text = "";
        if (KickGhost == null) Debug.LogError("KickGhost is not assigned.");
        if (SnareGhost == null) Debug.LogError("SnareGhost is not assigned.");
        if (HiHatGhost == null) Debug.LogError("HiHatGhost is not assigned.");
    }

    void Update()
    {
        if (Playing)
        {
            currentBeat = conductor.completedLoops;
            if (GhostFunction[currentBeat])
            {
                SpawnGhosts();
                GhostFunction[currentBeat] = false;
            }
            if (currentBeat < SongFunction.Length && SongFunction[currentBeat])
            {
                DespawnGhosts();
                Debug.Log("despawning!");
                Despawn();
                Debug.Log("spawning!");
                Spawn();
                Debug.Log("changing part!");
                currentPart++;
                SongFunction[currentBeat] = false;
            }
            scoreText.text = "Highscore: " + highscoreName + " " + highscore + "\nScore: " + Score;
        }
    }

    public void SetDifficulty(string difficulty)
    {
        Difficulty = difficulty;
        var StartButton = GameObject.FindWithTag("StartButton");
        // Initialize the arrays with the size of the SongParts enum
        int songPartsCount = System.Enum.GetValues(typeof(SongParts)).Length;
        KickPatterns = new bool[songPartsCount][];
        SnarePatterns = new bool[songPartsCount][];
        HiHatPatterns = new bool[songPartsCount][];

        switch (Difficulty)
        {
            case "Easy":
                KickPatterns = EasyKickPatterns;
                SnarePatterns = EasySnarePatterns;
                HiHatPatterns = EasyHiHatPatterns;
                StartButton.GetComponent<Button>().onClick.Invoke();
                break;

            case "Medium":
                KickPatterns = MediumKickPatterns;
                SnarePatterns = MediumSnarePatterns;
                HiHatPatterns = MediumHiHatPatterns;
                StartButton.GetComponent<Button>().onClick.Invoke();
                break;

            case "Hard":
                KickPatterns = HardKickPatterns;
                SnarePatterns = HardSnarePatterns;
                HiHatPatterns = HardHiHatPatterns;
                StartButton.GetComponent<Button>().onClick.Invoke();
                break;
        }
    }

    public void Spawn()
    {
        DespawnGhosts();
        KickDrum.GetComponent<DrumSpawnerScript>().DrumPattern = KickPatterns[SongStructure[currentPart]];
        SnareDrum.GetComponent<DrumSpawnerScript>().DrumPattern = SnarePatterns[SongStructure[currentPart]];
        HiHat.GetComponent<DrumSpawnerScript>().DrumPattern = HiHatPatterns[SongStructure[currentPart]];
        KickDrum.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(kickRadius);
        SnareDrum.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(snareRadius);
        HiHat.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(hiHatRadius);
    }

    public void SpawnGhosts()
    {
        KickGhost.GetComponent<DrumSpawnerScript>().DrumPattern = KickPatterns[SongStructure[currentPart]];
        SnareGhost.GetComponent<DrumSpawnerScript>().DrumPattern = SnarePatterns[SongStructure[currentPart]];
        HiHatGhost.GetComponent<DrumSpawnerScript>().DrumPattern = HiHatPatterns[SongStructure[currentPart]];
        KickGhost.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(kickRadius);
        SnareGhost.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(snareRadius);
        HiHatGhost.GetComponent<DrumSpawnerScript>().SpawnAroundPoint(hiHatRadius);

    }

    public void Despawn()
    {
        KickDrum.GetComponent<DrumSpawnerScript>().DeleteDrums();
        SnareDrum.GetComponent<DrumSpawnerScript>().DeleteDrums();
        HiHat.GetComponent<DrumSpawnerScript>().DeleteDrums();
    }
    public void DespawnGhosts()
    {
        KickGhost.GetComponent<DrumSpawnerScript>().DeleteDrums();
        SnareGhost.GetComponent<DrumSpawnerScript>().DeleteDrums();
        HiHatGhost.GetComponent<DrumSpawnerScript>().DeleteDrums();

    }

    public void AddScore(int score)
    {
        Score += score;
    }
    public void GameOver()
    {
        Playing = false;
        Debug.Log("Game Over");
        if (KickDrum == null) Debug.LogError("KickDrum is null");
        if (SnareDrum == null) Debug.LogError("SnareDrum is null");
        if (HiHat == null) Debug.LogError("HiHat is null");
        if (Circle == null) Debug.LogError("circle is null");
        if (Rotator == null) Debug.LogError("rotate is null");
        if (scoreText == null) Debug.LogError("scoreText is null");
        if (RestartButton == null) Debug.LogError("restartButton is null");
        KickDrum.GetComponent<DrumSpawnerScript>().DeleteDrums();
        SnareDrum.GetComponent<DrumSpawnerScript>().DeleteDrums();
        HiHat.GetComponent<DrumSpawnerScript>().DeleteDrums();
        if (Score > highscore)
        {
            highscore = Score;
            PlayerPrefs.SetString("HighscoreName", playerName);
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        scoreText.rectTransform.anchoredPosition = new Vector2(0, 0);
        scoreText.text = "GAME OVER\nFinal Score: " + Score;
        Debug.Log("text should change");
        Circle.gameObject.SetActive(false);
        Rotator.gameObject.SetActive(false);
        RestartButton.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(CountdownAndStartGame());
    }

    private IEnumerator CountdownAndStartGame()
    {
        SpawnGhosts();
        int countdown = 3; // Countdown from 3
        float bpm = conductor.bpm; // Get the BPM from the conductor
        float interval = 60f / bpm; // Calculate the interval based on BPM

        while (countdown > 0)
        {
            CountdownText.text = countdown.ToString();
            yield return new WaitForSeconds(interval);
            countdown--;
        }

        CountdownText.text = "Go!";
        yield return new WaitForSeconds(interval);

        CountdownText.text = "";
        DespawnGhosts();
        conductor.StartSong();
        Playing = true;
        playerName = nameText.text;
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.SetString("HighscoreName", "");
        highscore = 0;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
