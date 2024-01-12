using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public void OnRestartLevelButtonClick()
    {
        GameManager.Instance.RestartLevel();
    }

    public void OnResumeButtonClick()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OnMainMenuButtonClick()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
