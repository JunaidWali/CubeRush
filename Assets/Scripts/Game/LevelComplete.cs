using System;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{

	[SerializeField] private Animation endTransition;
	[SerializeField] private Camera endCamera;

	void OnTriggerEnter()
	{
		endCamera.enabled = true;
		endTransition.Play("LevelComplete");
		StartCoroutine(GameManager.Instance.LoadUI(GameManager.UIScene.UI_LevelComplete));
	}
}
