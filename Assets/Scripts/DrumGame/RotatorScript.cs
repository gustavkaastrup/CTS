using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RotatorScript : MonoBehaviour
{
    private ConductorScript conductor;
    public static RotatorScript instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        conductor = ConductorScript.instance;
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 360, ConductorScript.instance.loopPositionInAnalog));
    }
}
