using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnSinglePlayerButtonClick()
    {
        GameManager.Instance.SetCurrentGameMode(GameManager.GameMode.SinglePlayer);
        GameManager.Instance.StartGameAs();
    }

    public void OnMultiPlayerButtonClick()
    {
        GameManager.Instance.SetCurrentGameMode(GameManager.GameMode.MultiPlayer);
        GameManager.Instance.StartGameAs();
    }
}
