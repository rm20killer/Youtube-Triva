using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Playlist : MonoBehaviour
{
    public WhoAmI whoAmI;
    public BaseVideo[] video;
    public int currentVideoIndex = 0;

    public VideoPlayer videoPlayer;
    private List<int> playedvideo = new List<int>();
 
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
