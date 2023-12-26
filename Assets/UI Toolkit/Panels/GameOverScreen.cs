using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScreen : MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Main-Menu").clicked += () => { GameManager.LoadMainMenu(); };
        root.Q<Button>("Quit-Game").clicked += () => { GameManager.QuitGame(); };
    }
}
