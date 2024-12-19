using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Loader.Scene scene;
    private Loader loader;
    private UnlockableScript unlockableScript;

    private void Start()
    {
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<Loader>();
        unlockableScript = GetComponent<UnlockableScript>();
    }

    private void OnMouseDown()
    {
        if(unlockableScript != null){
            if (!unlockableScript.isLocked)
            loader.LoadScene(scene);
            SFXPlayerScript.instance.PlayButtonPress();
        } else {
            loader.LoadScene(scene);
        }
    }
}
