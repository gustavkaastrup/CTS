using UnityEngine;

public class DrumNoteScript : MonoBehaviour
{
    public string noteName;
    public int barIndex;
    public int beatIndex;
    public double hitWindowSeconds = 0.2f; // The time window in seconds to hit the note
    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;

    public double targetTime;
    private bool isHit = false;
    private bool isActive = false;
    private SpriteRenderer spriteRenderer;

    public void Initialize(string noteName, int barIndex, int beatIndex, double targetTime)
    {
        this.noteName = noteName;
        this.barIndex = barIndex;
        this.beatIndex = beatIndex;
        this.targetTime = targetTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetInactive();
    }

    void Update()
    {
        if (!isHit && Mathf.Abs(AudioMidiController.instance.currentTime - (float)targetTime) <= hitWindowSeconds)
        {
            if (!isActive)
            {
                SetActive();
            }
            CheckForKeyPress();
        } else if (!isHit && AudioMidiController.instance.currentTime > targetTime + hitWindowSeconds)
        {
            Destroy(gameObject);
        }
    }

    void CheckForKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.K) && noteName == "C")
        {
            HitNote();
        }
        else if (Input.GetKeyDown(KeyCode.S) && noteName == "D")
        {
            HitNote();
        }
        else if (Input.GetKeyDown(KeyCode.H) && noteName == "E")
        {
            HitNote();
        }
        else if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.H))
        {
            MissNote();
        }
    }

     void HitNote()
    {
        isHit = true;
        float timeDifference = AudioMidiController.instance.currentTime - (float)targetTime;
        DrumScoreManager.instance.AddScore(timeDifference);
        DrumPlayerSoundManager.instance.PlaySound(noteName);
        Destroy(gameObject);
    }

    void MissNote()
    {
        DrumPlayerSoundManager.instance.PlayMissSound();
        DrumScoreManager.instance.ResetCombo();
    }

    public void SetActive()
    {
        isActive = true;
        spriteRenderer.color = activeColor;
         transform.position = new Vector3(transform.position.x, transform.position.y, -1); 

    }

    public void SetInactive()
    {
        isActive = false;
        spriteRenderer.color = inactiveColor;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}