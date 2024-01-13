using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public void OnRestartLevelButtonClick()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.RestartLevel();
    }

    public void OnResumeButtonClick()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OnMainMenuButtonClick()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.LoadMainMenu();
    }
}
