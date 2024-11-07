using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions; 
using UnityEngine;
using UnityEngine.Serialization;
using System.Globalization;

public class WhoAmI : MonoBehaviour
{
    [FormerlySerializedAs("rightAnswer")] [SerializeField]
    private string _rightAnswer = "mosshorn";

    [SerializeField] private int maxPoints = 100;
    [SerializeField] private double maxSeconds = 120;
    [SerializeField] private DateTime _startTime;

    [SerializeField]
    private PlayerDataContainer _playerDataContainer;
    
    private HashSet<string> _correctPlayers = new HashSet<string>(); // Keep track of players who already answered correctly

    
    public bool testing = false;

    // private void Start()
    // {
    //     if (testing)
    //     {
    //         StartGame(_rightAnswer);
    //     }
    // }

    public void StartGame(string rightAnswer, int max, double secounds)
    {
        _rightAnswer = rightAnswer;
        maxPoints = max;
        maxSeconds = secounds;
        _startTime = DateTime.UtcNow;
    }

    public bool CheckQuess(string author, string message, string ImageURL, string publishedAt)
    {
         string normalizedMessage = message.Trim('"');
         normalizedMessage = Regex.Replace(normalizedMessage, @"\s+", ""); 
        if (string.Equals(normalizedMessage, _rightAnswer, StringComparison.OrdinalIgnoreCase)) 
        {
            string normalizedAuthor = author.Trim('"');
            string normalizedImageURL = ImageURL.Trim('"');
            string normalizedPublishedAt = publishedAt.Trim('"');
            if (_correctPlayers.Contains(normalizedAuthor))
            {
                Debug.Log($"{normalizedAuthor} has already answered correctly.");
                return false; // Ignore if the player already answered correctly
            }

            DateTime publishedTime = DateTime.Parse(normalizedPublishedAt);

            Debug.Log(_startTime);
            Debug.Log(publishedTime);
            if (publishedTime < _startTime)
            {
                Debug.Log($"{normalizedAuthor}'s answer was sent before the start time.");
                return false; // Ignore if published time is before start time
            }
            Debug.Log($"{normalizedMessage} sent by {author} was right");
            
            //add to PlayerDataContainer
            _playerDataContainer.AddPoints(normalizedAuthor, normalizedImageURL, CalculatePoints(publishedTime));
            _correctPlayers.Add(normalizedAuthor); 

            return true;
        }
        else
        {
            Debug.Log($"{normalizedMessage} sent by {author} was incorrect. Right awnser: {_rightAnswer}");
            return false;
        }
    }
    
    
    public int CalculatePoints(DateTime publishedAt)
    {
        Debug.Log(publishedAt);
        // Parse the publishedAt string into a DateTime object
        try
        {
            // Calculate the time difference in seconds
            TimeSpan timeDifference = _startTime - publishedAt;
            double totalSeconds = timeDifference.TotalSeconds;

            // Calculate points based on time difference
            if (totalSeconds <= 0)
            {
                // If published time is in the future, give maximum points
                return maxPoints;
            }
            else if (totalSeconds <= maxSeconds)
            {
                // Decrease points linearly over the time range
                double points = maxPoints * (1 - (totalSeconds / maxSeconds));
                return (int)Math.Round(points);
            }
            else
            {
                // If published time is older than the time range, give minimum points
                return 0; // Or a minimum value you define
            }
        }
        catch (FormatException ex)
        {
            Debug.LogError("Failed to parse publishedAt: " + publishedAt + "\nException: " + ex.Message);
            return 0; // Or handle the error as needed
        }
        
    }
}
