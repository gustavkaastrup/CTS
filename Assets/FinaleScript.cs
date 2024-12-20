using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FinaleScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!videoPlayer.isPlaying){
            SceneManager.LoadScene("MainMenu");
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("MainMenu");
        }
    }
}
