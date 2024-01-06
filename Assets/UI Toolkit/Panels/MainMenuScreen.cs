using System.Collections;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MonoBehaviour
{    
/*     public Animator transition;
    
    private float transitionTime = 1f; */
    
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Single-Player").clicked += () =>
        {
            GameManager.SetGameMode(GameManager.GameMode.SinglePlayer);
            StartCoroutine(SceneTransition.Instance.StartTransition());
            GameManager.StartGame();
        };
        root.Q<Button>("Multi-Player").clicked += () =>
        {
            GameManager.SetGameMode(GameManager.GameMode.MultiPlayer);
            StartCoroutine(SceneTransition.Instance.StartTransition());
            GameManager.StartGame();
        };
    }

    /* IEnumerator StartGame()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        GameManager.StartGame();
    } */
}
