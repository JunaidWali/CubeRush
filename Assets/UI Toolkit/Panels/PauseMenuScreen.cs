using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : MonoBehaviour
{

    //public static bool GameIsPaused;


    private void Awake()
    {
        Debug.Log("Awake method called in PauseMenuScreen");
        
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Main-Menu").clicked += () => { GameManager.LoadMainMenu(); };
        root.Q<Button>("Resume-Game").clicked += () => { GameManager.ResumeGame(); };
        root.Q<Button>("Restart-Level").clicked += () => { GameManager.RestartLevel(); };

        //GameIsPaused = false;
        
    }


/*     public void PauseGame()
    {
        pauseMenu.enabled = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
        Debug.Log("PauseGame() Called");
    }

    public void ResumeGame()
    {
        pauseMenu.enabled = false;
        GameIsPaused = false;
        Time.timeScale = 1f;
        Debug.Log("ResumeGame() Called");
    }

    public void RestartLevel()
    {
        //ResumeGame();
        GameManager.RestartLevel();
        Debug.Log("RestartLevel() Called");
    }

    public void LoadMainMenu()
    {
        //ResumeGame();
        GameManager.LoadMainMenu();
        Debug.Log("LoadMainMenu() Called");
    } */

}
