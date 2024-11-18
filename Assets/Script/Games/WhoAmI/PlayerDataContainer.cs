using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public string displayName;
    public string profileImageUrl;
    public int points;
}


public class PlayerDataContainer : MonoBehaviour
{
    public List<PlayerData> Data = new List<PlayerData>();

    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/playerData.json";
        LoadData();
    }

    public List<PlayerData> GetPlayerData()
    {
        return Data;
    }
    public void LoadData()
    {
        // Check if the save file exists
        if (File.Exists(saveFilePath))
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(saveFilePath);

            // Deserialize the JSON data back into this object
            JsonUtility.FromJsonOverwrite(jsonData, this);

            Debug.Log("Data loaded from: " + saveFilePath);
        }
        else
        {
            Debug.Log("Save file not found. Starting with empty data.");
        }
    }
    
    public void SaveData()
    {
        // Serialize the data to JSON
        string jsonData = JsonUtility.ToJson(this);

        // Write the JSON data to the file
        File.WriteAllText(saveFilePath, jsonData);

        Debug.Log("Data saved to: " + saveFilePath);
    }
    
    
    public void DeleteData()
    {
        // Clear the data list in memory
        Data.Clear();

        // Delete the save file
        if (File.Exists(saveFilePath))
        {
            
            File.Delete(saveFilePath);
            Debug.Log("Data deleted.");
        }
        else
        {
            Debug.Log("No save file to delete.");
        }
    }

    public void AddPoints(string author, string ImageURL, int points)
    {
        // Check if the author already exists in the data
        PlayerData player = Data.Find(p => p.displayName == author);

        if (player != null)
        {
            // Author exists, add points to their existing entry
            player.points += points;
            Debug.Log(author + " now has " + player.points + " points.");
        }
        else
        {
            // Author doesn't exist, create a new entry
            PlayerData newPlayer = new PlayerData { displayName = author, profileImageUrl = ImageURL, points = points };
            Data.Add(newPlayer);
            Debug.Log(author + " added with " + points + " points.");
        }

        // Optionally save the updated data to the file
        SaveData();
    }
}