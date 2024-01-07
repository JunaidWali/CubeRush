using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void SinglePlayerButton()
    {
        GameManager.SetGameMode(GameManager.GameMode.SinglePlayer);
        GameManager.StartGame();
    }

    public void MultiPlayerButton()
    {
        GameManager.SetGameMode(GameManager.GameMode.MultiPlayer);
        GameManager.StartGame();
    }
}
