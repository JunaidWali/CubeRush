using TMPro;
using UnityEngine;

public class UI_LevelComplete : MonoBehaviour
{
    private string winnerName;
    private int winnerPlacement;
    [SerializeField] private TextMeshProUGUI winnerText;


    void Awake()
    {
        winnerName = GameObject.Find("END").GetComponent<LevelComplete>().playerName;
        winnerPlacement = GameObject.Find("END").GetComponent<LevelComplete>().playerPlacement;
    }
    void Start()
    {
        AudioManager.Instance.StopAllExcept("LevelTheme");
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolumeSilenced);
        switch (winnerPlacement)
        {
            case 1:
                winnerText.text = $"{winnerPlacement}st Place: {winnerName}";
                break;
            case 2:
                winnerText.text = $"{winnerPlacement}nd Place: {winnerName}";
                break;
            case 3:
                winnerText.text = $"{winnerPlacement}rd Place: {winnerName}";
                break;
            case 4:
                winnerText.text = $"{winnerPlacement}th Place: {winnerName}";
                break;
        }
    }

    public void OnRestartLevelButtonClick()
    {
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.RestartLevel();
    }

    public void OnNextLevelButtonClick()
    {
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.LoadNextLevel();
    }

    public void OnMainMenuButtonClick()
    {
        AudioManager.Instance.StopAll();
        AudioManager.Instance.SetVolume("LevelTheme", AudioManager.Instance.levelThemeVolume);
        AudioManager.Instance.Play("ButtonClick");
        StartCoroutine(GameManager.Instance.LoadUI(GameManager.UIScene.UI_MainMenu));
    }
}
