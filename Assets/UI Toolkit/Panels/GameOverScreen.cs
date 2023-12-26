using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScreen : MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Main-Menu").clicked += () => Debug.Log("Main-Menu clicked");
        root.Q<Button>("Quit-Game").clicked += () => Debug.Log("Quit-Game clicked");
    }
}
