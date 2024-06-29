using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class LeaderBoardUI : MonoBehaviour
{
    public static LeaderBoardUI Instance { get; private set; }
    
    [SerializeField] private Button backButton;

    private void Awake()
    {
        Instance = this;
        Hide();
        backButton.onClick.AddListener(() => {
            Hide();
            MainMenuUI.Instance.Show();
        });
    }

    public void Show() {
        gameObject.SetActive(true);
    }
    
    private void Hide() {
        gameObject.SetActive(false);
    }
}
