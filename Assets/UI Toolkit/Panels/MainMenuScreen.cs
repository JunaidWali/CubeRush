using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Single-Player").clicked += () => Debug.Log("Single-Player clicked");
        root.Q<Button>("Multi-Player").clicked += () => Debug.Log("Multi-Player clicked");
    }



}
