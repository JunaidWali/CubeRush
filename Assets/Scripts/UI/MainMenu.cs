using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnSinglePlayerButtonClick()
    {
        GameManager.Instance.StartGameAs(GameManager.GameMode.SinglePlayer);
    }

    public void OnMultiPlayerButtonClick()
    {
        GameManager.Instance.StartGameAs(GameManager.GameMode.MultiPlayer);
    }
}
