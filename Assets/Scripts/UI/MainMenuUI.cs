using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    
    public static MainMenuUI Instance { get; private set; }
    
    [SerializeField] private HighScoreTable highScoreTable;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button signInButton;
    [SerializeField] private Button leaderBoardButton;
    [SerializeField] private Button signOutButton;
    [SerializeField] private TextMeshProUGUI playerNameText;


    private void Awake()
    {
        Instance = this;
        playerNameText.gameObject.SetActive(false);
        // PlayerPrefs.DeleteAll();
        // DataManager.DeleteAllData();
        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
        signInButton.onClick.AddListener(() =>
        {
            SignInUI.Instance.Show();
            Hide();
        });
        signOutButton.onClick.AddListener(() =>
        {
            SignOut();
        });
        leaderBoardButton.onClick.AddListener(() =>
        {
            highScoreTable.LoadData();
            LeaderBoardUI.Instance.Show();
            HighScoreTable.Instance.LoadData();
            Hide();
        });
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (DataManager.name == null)
        {
            playerNameText.gameObject.SetActive(false);
            signOutButton.gameObject.SetActive(false);
            signInButton.gameObject.SetActive(true);
        }
        else
        {
            playerNameText.text = "Welcome " + DataManager.name;
            playerNameText.gameObject.SetActive(true);
            signOutButton.gameObject.SetActive(true);
            signInButton.gameObject.SetActive(false);
        }
    }

    public void Show() {
        gameObject.SetActive(true);
        // resumeButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    
    private void SignOut()
    {
        DataManager.name = null;
        DataManager.highScore = 0;
    }
    
}