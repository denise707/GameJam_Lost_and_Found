using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    [SerializeField] VideoPlayer vp;
    private bool videoFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isVideoFinished())
        {
            videoFinished = false;
            SceneManager.LoadScene("Game Proper");
        }
    }

    bool isVideoFinished()
    {
        videoFinished = !(vp.isPlaying);
        return videoFinished;
    }
}
