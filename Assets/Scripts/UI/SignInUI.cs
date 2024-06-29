using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using TMPro;

public class SignInUI : MonoBehaviour
{
    public static SignInUI Instance { get; private set; }

    [SerializeField] private TMP_InputField userNameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private Button signInButton;
    [SerializeField] private Button signUpButton;
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    private void Awake()
    {
        Instance = this;
        Hide();
        signInButton.onClick.AddListener(() => {
            SignIn();
        });
        backButton.onClick.AddListener(() => {
            Hide();
            MainMenuUI.Instance.Show();
        });
        signUpButton.onClick.AddListener(() => {
            Hide();
            SignUpUI.Instance.Show();
        });
    }
    
    public void Show() {
        warningText.text = "";
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
        warningText.text = "";
        userNameField.text = "";
        passwordField.text = "";
    }
    
    private void SignIn() {
        warningText.text = "";
        if (PlayerPrefs.HasKey(userNameField.text))
        {
            if (passwordField.text == PlayerPrefs.GetString(userNameField.text))
            {
                DataManager.name = userNameField.text;
                playerNameText.text = "Welcome " + DataManager.name;
                DataManager.highScore = DataManager.FindPlayerDataByName(DataManager.name);
                Hide();
                MainMenuUI.Instance.Show();
            }
            else
            {
                warningText.text = "Invalid Username or Password";
            }
        }
        else
        {
            warningText.text = "Invalid Username or Password";
        }
    }
}
