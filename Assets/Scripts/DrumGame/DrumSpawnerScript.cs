using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrumSpawnerScript : MonoBehaviour
{


    [Header("Setup")]
    public bool[] DrumPattern;
    public int subdivisions;
    public GameObject Drum;
    public float radius;
    public bool isSpawning = false;
    private List<GameObject> spawnedDrums = new List<GameObject>();


    public void SpawnAroundPoint(float radius)
    {
        float angleStep = 360f / subdivisions;

        for (int i = 0; i < DrumPattern.Length; i++)
        {
            if (DrumPattern[i])
            {
                float angle = i * angleStep;
                Vector3 position = new Vector3(
                    Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                    Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                    0
                );

                GameObject spawnedDrum = Instantiate(Drum, position, Quaternion.identity);
                spawnedDrums.Add(spawnedDrum);
            }
        }
    }

    public void DeleteDrums()
    {
        foreach (GameObject drum in spawnedDrums)
        {
            Destroy(drum);
        }
        spawnedDrums.Clear();
    }
}
