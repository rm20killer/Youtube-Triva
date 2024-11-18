using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private PlayerDataContainer _playerDataContainer;
    public List<PlayerData> Top5 = new List<PlayerData>();
    //leaderboard stuff
    //- 5 name, 5 points, 5 PFP

    [SerializeField] private TextMeshProUGUI[] _names;
    [SerializeField] private TextMeshProUGUI[] _points;
    [SerializeField] private Image[] _images;
    
    // public Image _EventTexture;
    private void Update()
    {
        UpdateLeaderboard();
    }

    void FindTop5()
    {
        List<PlayerData> data = _playerDataContainer.GetPlayerData();
        //find top 5
        data.Sort((a, b) => b.points.CompareTo(a.points));
        Top5 = new List<PlayerData>(data.Take(5));
    }

    void UpdateLeaderboard()
    {
        FindTop5();
        //if FindTop5 is less then 5 only update that amount
        if (Top5.Count < 5)
        {
            for (int i = Top5.Count; i < 5; i++)
            {
                _names[i].text = "";
                _points[i].text = "";
            }
            for (int i = 0; i < Top5.Count; i++)
            {
                _names[i].text = Top5[i].displayName;
                _points[i].text = Top5[i].points.ToString();
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                _names[i].text = Top5[i].displayName;
                _points[i].text = Top5[i].points.ToString();
            }
        }
        
    }
        

}
