using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class deprecated_CircleScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float pumpSpeed = 10.0f;
    public float sawtoothWave;
    private deprecated_ConductorScript conductor;
    private Vector3 originalScale;
    public static deprecated_CircleScript instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        originalScale = transform.localScale;
        conductor = deprecated_ConductorScript.instance;
    }

    // Update is called once per frame
    void Update()
    {

        float songPosition = conductor.loopPositionInBeats;
        sawtoothWave = 1f - ((songPosition * pumpSpeed) % 1f);
        float scaleFactor = 1 + sawtoothWave;
        transform.localScale = originalScale * scaleFactor;
    }
}
