using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_CountdownController : MonoBehaviour
{
    private int countdownTime;
    public TextMeshProUGUI countdownDisplay;
    private AudioSource countdownNumberAudio;
    private AudioSource countdownGoAudio;

    void Awake()
    {
        countdownNumberAudio = AudioManager.Instance.GetSource("CountdownNumber");
        countdownGoAudio = AudioManager.Instance.GetSource("CountdownGO");
    }

    private void Start()
    {
        countdownDisplay.gameObject.SetActive(false);
    }

    public IEnumerator CountdownToStart(Action callback)
    {
        countdownDisplay.gameObject.SetActive(true);

        countdownTime = 3;

        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            countdownNumberAudio.Play();

            yield return new WaitForSecondsRealtime(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        countdownGoAudio.Play();

        yield return new WaitForSecondsRealtime(0.5f);

        countdownDisplay.gameObject.SetActive(false);

        callback?.Invoke();
    }

    public IEnumerator CountdownToStart()
    {
        countdownDisplay.gameObject.SetActive(true);

        countdownTime = 3;

        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            countdownNumberAudio.Play();

            yield return new WaitForSecondsRealtime(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        countdownGoAudio.Play();

        yield return new WaitForSecondsRealtime(0.5f);
        countdownDisplay.gameObject.SetActive(false);
    }
}
