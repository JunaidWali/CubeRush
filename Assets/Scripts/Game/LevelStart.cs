using System.Collections;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    private UI_CountdownController countdownController;
    private UI_PauseMenu pauseMenu;

    void Awake()
    {
        countdownController = FindObjectOfType<UI_CountdownController>();
        pauseMenu = FindObjectOfType<UI_PauseMenu>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (!AudioManager.Instance.IsPlaying("LevelTheme"))
        {
            AudioManager.Instance.PlayDelayed("LevelTheme", 2f);
        }
        GameManager.Instance.isLevelCompleted = false;
        yield return new WaitForSeconds(8f);
        StartCoroutine(countdownController.CountdownToStart(SetCountdownFinishedToTrue));


    }

    private void SetCountdownFinishedToTrue()
    {
        pauseMenu.isCountdownFinished = true;
    }
}