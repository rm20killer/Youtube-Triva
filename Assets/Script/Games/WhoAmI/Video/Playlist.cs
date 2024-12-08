using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Playlist : MonoBehaviour
{
    public WhoAmI whoAmI;
    public BaseVideo[] video;
    public int currentVideoIndex = 0;

    public VideoPlayer videoPlayer;
    private List<int> playedvideo = new List<int>();

    public GameObject Leaderboard;
    public GameObject VideoGameObject;
    public bool Playing = false;

    public GameObject StartUp;
    public int[] skip;
    private void Start()
    {
        //StartFirstGame();
    }

    public void StartFirstGame()
    {
        VideoGameObject.SetActive(true);
        videoPlayer.clip = video[currentVideoIndex].VideoClip;
        whoAmI.StartGame(video[currentVideoIndex].rightAnswer, video[currentVideoIndex].MaxPoints,
            video[currentVideoIndex].maxSeconds);
        videoPlayer.Play();
        Playing = true;
        Debug.Log((long)videoPlayer.clip.frameCount - 1);
        CloseMenu();
    }
    private void Update()
    {
        if(currentVideoIndex==8)
        {
            Leaderboard.SetActive(true);
            whoAmI.StopGame();
            return;
        }
        if (!Playing) return;
        //if the frame is the last frame of the video
        //Debug.Log(videoPlayer.frame);
        if (videoPlayer.frame == (long)videoPlayer.clip.frameCount - 1)
        {
   
            if (!video[currentVideoIndex].skip)
            {
                Openleaderboard();
            }
            else
            {
                PlayNextVideo();
            }
        }
        // Debug.Log(videoPlayer.frame); 1515
        
        // if(!videoPlayer.isPlaying)
        // {
        //     Leaderboard.SetActive(true);
        //     VideoGameObject.SetActive(false);
        //     whoAmI.StopGame();
        // }
        
    }

    private void Openleaderboard()
    {
        Leaderboard.SetActive(true);
        VideoGameObject.SetActive(false);
        whoAmI.StopGame();
        Playing = false;
    }
    public void PlayNextVideo()
    {
        if (PlayNext())
        {
            VideoGameObject.SetActive(true);
            Leaderboard.SetActive(false);
            whoAmI.StartGame(video[currentVideoIndex].rightAnswer, video[currentVideoIndex].MaxPoints,
                video[currentVideoIndex].maxSeconds);
            videoPlayer.Play();
            Playing = true;
        }
        else
        {
            Leaderboard.SetActive(true);
            VideoGameObject.SetActive(false);
            whoAmI.StopGame();
            Playing = false;
            Debug.Log("Game finished");
        }
    }
    /// <summary>
    /// play next video
    /// </summary>
    /// <returns>true if last video</returns>
    public bool PlayNext()
    {
        playedvideo.Add(currentVideoIndex);
        currentVideoIndex++;
        Debug.Log(currentVideoIndex);
        videoPlayer.clip = video[currentVideoIndex].VideoClip;
        return currentVideoIndex <= video.Length;
    }
    
    public void CloseMenu()
    {
        StartUp.SetActive(false);
    }


    public void SetIndex(int value)
    {
        currentVideoIndex = value;
    }
}
