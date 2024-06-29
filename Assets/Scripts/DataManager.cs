using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DataManager
{
    public static string name = null;
    public static int highScore = 0;

    // Path to store the JSON file
    public static string dataPath = Application.persistentDataPath + "/playerData.json";

    // Save player data to JSON file
    public static void SavePlayerData(PlayerData playerData)
    {
        string jsonData = "{\n    \"Items\": [" + JsonUtility.ToJson(playerData) + "]\n}";
        File.WriteAllText(dataPath, jsonData);
    }
    public static int FindPlayerDataByName(string nameToFind)
    {
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            if (jsonData == "")
            {
                Debug.LogError("No player data found!");
                return 0;
            }
            PlayerData[] playerDataArray = JsonHelper.FromJson<PlayerData>(jsonData);

            // Iterate over player data array to find matching playerName
            foreach (PlayerData playerData in playerDataArray)
            {
                if (playerData.playerName == nameToFind)
                {
                    Debug.Log("Player Name: " + playerData.playerName + ", Score: " + playerData.playerScore);
                    return playerData.playerScore;
                }
            }

            Debug.Log("Player with name '" + nameToFind + "' not found!");
        }
        else
        {
            Debug.LogError("No player data found!");
        }

        return 0;
    }
    public static void ChangePlayerDataByName(string nameToFind, int highScore)
    {
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            if (jsonData == "")
            {
                Debug.LogError("No player data found!");
                return;
            }
            PlayerData[] playerDataArray = JsonHelper.FromJson<PlayerData>(jsonData);

            // Iterate over player data array to find matching playerName
            foreach (PlayerData playerData in playerDataArray)
            {
                if (playerData.playerName == nameToFind)
                {
                    playerData.playerScore = highScore;
                    Debug.Log(playerData.playerName + " " + playerData.playerScore);
                    break;
                }
            }
            string updatedJsonData = JsonHelper.ToJson(playerDataArray, true);

            // Write the updated JSON data back to the file, overwriting the existing content
            File.WriteAllText(dataPath, updatedJsonData);

            Debug.Log("Data appended successfully!");
        }
        else
        {
            Debug.LogError("No player data found!");
        }
    }
    public static void AppendPlayerData(PlayerData newPlayerData)
    {
        // Check if the file exists
        if (File.Exists(dataPath))
        {
            // Read the existing JSON data
            string jsonData = File.ReadAllText(dataPath);
            // Deserialize the JSON data into an array of PlayerData
            if (jsonData == "")
            {
                SavePlayerData(newPlayerData);
                return;
            }
            
            PlayerData[] playerDataArray = JsonHelper.FromJson<PlayerData>(jsonData);

            // Create a new array to hold the updated data (original + new data)
            PlayerData[] updatedPlayerData;
            if (playerDataArray == null || playerDataArray.Length == 0)
            {

                updatedPlayerData = new PlayerData[1];
            }
            else 
                updatedPlayerData = new PlayerData[playerDataArray.Length + 1];
            // Copy the existing data to the new array
            for (int i = 0; i < updatedPlayerData.Length - 1; i++)
            {
                updatedPlayerData[i] = playerDataArray[i];
            }

            // Append the new data
            updatedPlayerData[^1] = newPlayerData;

            // Serialize the updated data back into JSON format
            string updatedJsonData = JsonHelper.ToJson(updatedPlayerData, true);

            // Write the updated JSON data back to the file, overwriting the existing content
            File.WriteAllText(dataPath, updatedJsonData);

            Debug.Log("Data appended successfully!");
        }
        else
        {
            Debug.LogError("File not found! Cannot append data.");
        }
    }
    
    public static void DeletePlayerData(string playerNameToDelete)
    {
        if (File.Exists(dataPath))
        {
            // Read the existing JSON data
            string jsonData = File.ReadAllText(dataPath);

            // Deserialize the JSON data into a list of PlayerData
            List<PlayerData> playerDataList = new List<PlayerData>(JsonUtility.FromJson<PlayerData[]>(jsonData));

            // Find the player data with the specified name and remove it
            PlayerData playerToDelete = playerDataList.Find(p => p.playerName == playerNameToDelete);
            if (playerToDelete != null)
            {
                playerDataList.Remove(playerToDelete);
                Debug.Log("Player data for '" + playerNameToDelete + "' deleted successfully!");
            }
            else
            {
                Debug.LogWarning("Player data for '" + playerNameToDelete + "' not found!");
            }

            // Serialize the updated data back into JSON format
            string updatedJsonData = JsonUtility.ToJson(playerDataList.ToArray());

            // Write the updated JSON data back to the file, overwriting the existing content
            File.WriteAllText(dataPath, updatedJsonData);
        }
        else
        {
            Debug.LogError("File not found! Cannot delete player data.");
        }
    }
    public static void DeleteAllData()
    {
        // Overwrite the file with an empty array
        File.WriteAllText(dataPath, "");
        Debug.Log("All data deleted successfully!");
    }
    
}
