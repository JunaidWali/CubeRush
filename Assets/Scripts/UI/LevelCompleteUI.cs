using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    public void OnRestartLevelButtonClick()
    {
        GameManager.Instance.RestartLevel();
    }

    public void OnNextLevelButtonClick()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void OnMainMenuButtonClick()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
