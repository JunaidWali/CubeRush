using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!AudioManager.Instance.IsPlaying("LevelTheme"))
        {
            AudioManager.Instance.PlayDelayed("LevelTheme", 2f);
        }
    }
}
