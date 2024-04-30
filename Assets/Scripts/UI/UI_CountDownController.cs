using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_CountdownController : MonoBehaviour
{
    private int countdownTime;
    public TextMeshProUGUI countdownDisplay;

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

            yield return new WaitForSecondsRealtime(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";

        yield return new WaitForSecondsRealtime(0.3f);

        countdownDisplay.gameObject.SetActive(false);

        callback?.Invoke();
    }
}
