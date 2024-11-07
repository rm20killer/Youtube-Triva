
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Video", menuName = "ScriptableObjects/Video")]
public class BaseVideo : ScriptableObject
{
    public string rightAnswer;
    public VideoClip VideoClip;
    public int MaxPoints = 1000;
    public double maxSeconds = 120;
}
