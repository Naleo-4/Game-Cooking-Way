using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class HighScoreTable : MonoBehaviour
{
    // [Serializable]
    // class DataList
    // {
    //     public List<PlayerData> playerDataList;
    // }
    public static HighScoreTable Instance { get; private set; }
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    private PlayerData[] playerDataList;
    private List<Transform> highScoreEntryTransformList;

    private void Awake()
    {
        Instance = this;
        entryContainer = transform.Find("Container");
        entryTemplate = entryContainer.Find("Template");
        entryTemplate.gameObject.SetActive(false);
        LoadData();
    }

    private void Start()
    {
        entryContainer = transform.Find("Container");
        entryTemplate = entryContainer.Find("Template");

        entryTemplate.gameObject.SetActive(false);
        LoadData();
    }

    public void LoadData()
    {
        ClearData();
        string jsonData = File.ReadAllText(DataManager.dataPath);
        
        if (jsonData != "")
        {
            playerDataList = JsonHelper.FromJson<PlayerData>(jsonData);
            for (int i = 0; i < playerDataList.Length-1; i++)
            {
                for (int j = i + 1; j < playerDataList.Length; j++)
                {
                    if (playerDataList[j].playerScore > playerDataList[i].playerScore)
                    {
                        (playerDataList[i], playerDataList[j]) = (playerDataList[j], playerDataList[i]);
                    }
                }
            }

            highScoreEntryTransformList = new List<Transform>();
            foreach (var playerData in playerDataList)
            {
                CreateHighScoreEntryTransform(playerData, entryContainer, highScoreEntryTransformList);
            }
        }
    }

    public void ClearData()
    {
        playerDataList = null;
        if (highScoreEntryTransformList != null){
            foreach (var entry in highScoreEntryTransformList)
            {
                Destroy(entry.gameObject);
            }
        }
        else return;
        highScoreEntryTransformList.Clear();
    }

    private void CreateHighScoreEntryTransform(PlayerData playerData, Transform container,
        List<Transform> transformList)
    {
        float templateHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, 125 - templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString = rank.ToString();
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rankString + "TH";
                break;
        }

        entryTransform.Find("Position").GetComponent<TMPro.TextMeshProUGUI>().text = rankString;
        entryTransform.Find("HighScore").GetComponent<TMPro.TextMeshProUGUI>().text = playerData.playerScore.ToString();
        entryTransform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = playerData.playerName;
        bool test = (rank % 2 == 1) ? true : false;
        entryTransform.Find("Image").gameObject.SetActive(test);
        transformList.Add(entryTransform);
    }
}