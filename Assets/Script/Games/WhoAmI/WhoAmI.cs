using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions; 
using UnityEngine;

public class WhoAmI : MonoBehaviour
{
    private string rightAnswer = "mosshorn";

    public bool CheckQuess(string author, string message, string ImageURL)
    {
         string normalizedMessage = message.Trim('"');
         normalizedMessage = Regex.Replace(normalizedMessage, @"\s+", ""); 
        if (string.Equals(normalizedMessage, rightAnswer, StringComparison.OrdinalIgnoreCase)) 
        {
            Debug.Log($"{normalizedMessage} sent by {author} was right");
            return true;
        }
        else
        {
            Debug.Log($"{normalizedMessage} sent by {author} was incorrect. Right awnser: {rightAnswer}");
            return false;
        }
    }
}
