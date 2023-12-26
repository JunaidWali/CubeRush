using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Single-Player").clicked += () =>
        {
            GameManager.SetGameMode(GameManager.GameMode.SinglePlayer);
            GameManager.StartGame();
        };
        root.Q<Button>("Multi-Player").clicked += () =>
        {
            GameManager.SetGameMode(GameManager.GameMode.MultiPlayer);
            GameManager.StartGame();
        };
    }
}
