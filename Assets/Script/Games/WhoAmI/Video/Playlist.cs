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
 
    private void Start()
    {
        videoPlayer.clip = video[currentVideoIndex].VideoClip;
        whoAmI.StartGame(video[currentVideoIndex].rightAnswer, video[currentVideoIndex].MaxPoints,
            video[currentVideoIndex].maxSeconds);
    }
    private void Update()
    {
        //if the music stopped playing
        if(!videoPlayer.isPlaying)
        {
            Leaderboard.SetActive(true);
            VideoGameObject.SetActive(false);
            whoAmI.StopGame();
        }
    }

    public void PlayNextVideo()
    {
        if (PlayNext())
        {
            VideoGameObject.SetActive(true);
            Leaderboard.SetActive(false);
            whoAmI.StartGame(video[currentVideoIndex].rightAnswer, video[currentVideoIndex].MaxPoints,
                video[currentVideoIndex].maxSeconds);
        }
        else
        {
            Leaderboard.SetActive(true);
            VideoGameObject.SetActive(false);
            whoAmI.StopGame();
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
