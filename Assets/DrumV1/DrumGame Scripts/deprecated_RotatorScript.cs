using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class deprecated_RotatorScript : MonoBehaviour
{
    private deprecated_ConductorScript conductor;
    public static deprecated_RotatorScript instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        conductor = deprecated_ConductorScript.instance;
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 360, deprecated_ConductorScript.instance.loopPositionInAnalog));
    }
}
