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
        } else if (!isHit && AudioMidiController.instance.currentTime > targetTime + hitWindowSeconds)
        {
            Destroy(gameObject);
            DrumScoreManager.instance.ResetCombo();
        }
    }
    
    public void HitNote()
    {
        if (isHit) return; // Prevent multiple activations

        isHit = true;
        float timeDifference = AudioMidiController.instance.currentTime - (float)targetTime;
        DrumScoreManager.instance.AddScore(timeDifference);
        DrumPlayerSoundManager.instance.PlaySound(noteName);
        Destroy(gameObject);
    }



    public void SetActive()
    {
        isActive = true;
        spriteRenderer.color = activeColor;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        DrumInputManager.instance.RegisterActiveNote(this); 

    }

    public void SetInactive()
    {
        isActive = false;
        spriteRenderer.color = inactiveColor;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        DrumInputManager.instance.UnregisterActiveNote(this);
    }
    void OnDestroy()
    {
        DrumInputManager.instance.UnregisterActiveNote(this);
    }
}