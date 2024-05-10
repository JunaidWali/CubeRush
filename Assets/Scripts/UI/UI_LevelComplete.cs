using TMPro;
using UnityEngine;

public class UI_LevelComplete : MonoBehaviour
{
    private string winnerName;
    private int winnerPlacement;
    [SerializeField] private TextMeshProUGUI winnerText;
    private AudioSource levelThemeAudio;
    private AudioSource buttonClickAudio;


    void Awake()
    {
        winnerName = GameObject.Find("END").GetComponent<LevelComplete>().playerName;
        winnerPlacement = GameObject.Find("END").GetComponent<LevelComplete>().playerPlacement;
        levelThemeAudio = AudioManager.Instance.GetSource("LevelTheme");
        buttonClickAudio = AudioManager.Instance.GetSource("ButtonClick");
    }
    void Start()
    {
        AudioManager.Instance.StopAllExcept("LevelTheme");
        levelThemeAudio.volume = AudioManager.Instance.silencedVolume;
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
        levelThemeAudio.volume = AudioManager.Instance.defaultVolume;
        buttonClickAudio.Play();
        GameManager.Instance.RestartLevel();
    }

    public void OnNextLevelButtonClick()
    {
        levelThemeAudio.volume = AudioManager.Instance.defaultVolume;
        buttonClickAudio.Play();
        GameManager.Instance.LoadNextLevel();
    }

    public void OnMainMenuButtonClick()
    {
        AudioManager.Instance.StopAll();
        levelThemeAudio.volume = AudioManager.Instance.defaultVolume;
        buttonClickAudio.Play();
        StartCoroutine(GameManager.Instance.LoadUI(GameManager.UIScene.UI_MainMenu));
    }
}
