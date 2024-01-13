using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    public void OnRestartLevelButtonClick()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.RestartLevel();
    }

    public void OnNextLevelButtonClick()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.LoadNextLevel();
    }

    public void OnMainMenuButtonClick()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.LoadMainMenu();
    }
}
