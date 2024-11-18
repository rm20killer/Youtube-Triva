using System.Collections;
using System.Collections.Generic;
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
    private void Start()
    {
        // StartFirstGame();
    }

    public void StartFirstGame()
    {
        videoPlayer.clip = video[currentVideoIndex].VideoClip;
        whoAmI.StartGame(video[currentVideoIndex].rightAnswer, video[currentVideoIndex].MaxPoints,
            video[currentVideoIndex].maxSeconds);
        videoPlayer.Play();
        Debug.Log((long)videoPlayer.clip.frameCount - 1);
    }
    private void Update()
    {
        if (!Playing) return;
        //if the frame is the last frame of the video
        if (videoPlayer.frame == (long)videoPlayer.clip.frameCount - 1)
        {
            Openleaderboard();
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
        return currentVideoIndex >= video.Length;
    }
    
    
    
}
