using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignUpUI : MonoBehaviour
{
    public static SignUpUI Instance { get; private set; }

    [SerializeField] private TMP_InputField userNameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private Button signUpButton;
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI signUpWarningText;
    private void Awake()
    {
        Instance = this;
        Hide();
        signUpButton.onClick.AddListener(() => {
            SignUp();
        });
        backButton.onClick.AddListener(() => {
            Hide();
            SignInUI.Instance.Show();
        });
    }
    
    public void Show() {
        signUpWarningText.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
        signUpWarningText.gameObject.SetActive(false);
        userNameField.text = "";
        passwordField.text = "";
    }
    
    private void SignUp()
    {
        if (PlayerPrefs.HasKey(userNameField.text))
        {
            signUpWarningText.text = "Username already exists!";
            signUpWarningText.gameObject.SetActive(true);
            return;
        }
        PlayerPrefs.SetString(userNameField.text, passwordField.text);
        DataManager.AppendPlayerData(new PlayerData { playerName = userNameField.text, playerScore = 0 });
        signUpWarningText.text = "Sign up successful!";
        signUpWarningText.gameObject.SetActive(true);
        PlayerPrefs.Save();
    }
}
