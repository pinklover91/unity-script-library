using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

/*
Drag and drop to your Video Player GameObject:
1. It automatically clear the texture after you pause or stop the videoPlayer
2. Use onVideoFinished to add your function when the video finished
*/


[RequireComponent(typeof(VideoPlayer))]
public class VideoManager : MonoBehaviour
{
    public static VideoManager current;
    RenderTexture renderTexture;
    public VideoPlayer videoPlayer;
    public UnityEvent onVideoFinished;

    public void Awake()
    {
        current = this;
		if(videoPlayer==null)
		{
			videoPlayer = this.GetComponent<VideoPlayer>();
		}
        renderTexture = videoPlayer.targetTexture;
        videoPlayer.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
      
        if(onVideoFinished!=null)
        {
            onVideoFinished.Invoke();
        }
    }
    public void Clean()
    {
        
        renderTexture.DiscardContents();
        RenderTexture rt = UnityEngine.RenderTexture.active;
        UnityEngine.RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.black);
        UnityEngine.RenderTexture.active = rt;

    }
    public bool isPlaying()
    {
        return videoPlayer.isPlaying;
    }
    
    public long frame()
    {
        return videoPlayer.frame;
    }
    public ulong frameCount()
    {
        return videoPlayer.frameCount;
    }
    public void Pause()
    {
        if (!videoPlayer.isPaused)
        {
            videoPlayer.Pause();
       
        }
            
        else
            videoPlayer.Play();
    }
    public void OnDisable()
    {
        Clean();
    }
    public void Stop()
    {
        videoPlayer.Stop();
    }
    // Start is called before the first frame update
    public void PlayURL(string urlPath)
    {
        videoPlayer.Stop();
        Clean();
       
        videoPlayer.url = urlPath;
        videoPlayer.Play();
      
        
    }
}
