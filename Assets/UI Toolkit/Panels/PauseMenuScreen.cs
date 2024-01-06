using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : MonoBehaviour
{

    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Main-Menu").clicked += () =>
        {
            StartCoroutine(SceneTransition.Instance.StartTransition());
            GameManager.LoadMainMenu();
        };
        root.Q<Button>("Resume-Game").clicked += () => GameManager.ResumeGame();
        root.Q<Button>("Restart-Level").clicked += () =>  GameManager.RestartLevel();
    }
}
