using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSpawnerScript : MonoBehaviour
{


    [Header("Setup")]
    public bool[] DrumPattern;
    public GameObject Drum;
    public float radius;

    public int subdivisions = 8;

    public bool isSpawning = false;


    // Start is called before the first frame update
    void Update()
    {
        if (isSpawning)
        {
            SpawnAroundPoint(radius, Drum, DrumPattern);
            isSpawning = false;
        }
    }

    void SpawnAroundPoint(float radius, GameObject Drum, bool[] pattern)
    {
        float angleStep = 360f / subdivisions;

        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i])
            {
                float angle = i * angleStep;
                Vector3 position = new Vector3(
                    Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                    Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                    0
                );

                Instantiate(Drum, position, Quaternion.identity);
            }
        }
    }
    public void StartSpawning()
    {
        isSpawning = true;
    }
}
