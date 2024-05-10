using System.Collections;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    private UI_CountdownController countdownController;
    private UI_PauseMenu pauseMenu;
    private FollowPlayer followPlayer;
    private AudioSource levelTheme;

    void Awake()
    {
        countdownController = FindObjectOfType<UI_CountdownController>();
        pauseMenu = FindObjectOfType<UI_PauseMenu>();
        followPlayer = AudioManager.Instance.GetComponent<FollowPlayer>();
        levelTheme = AudioManager.Instance.GetSource("LevelTheme");
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        followPlayer.enabled = true;
        if (!levelTheme.isPlaying)
        {
            levelTheme.Play();
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