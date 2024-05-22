using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class RebindScript : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls; // Assign this in the inspector
    [SerializeField] private List<Button> rebindButtons; // Assign these in the inspector
    [SerializeField] private List<TextMeshProUGUI> rebindButtonTexts; // Assign these in the inspector

    private List<InputAction> playerActions;

    private void Awake()
    {
        playerActions = new List<InputAction>
        {
            playerControls.FindAction("moveRight"),
            playerControls.FindAction("moveLeft"),
            playerControls.FindAction("Jump"),
            playerControls.FindAction("Boost"),
            playerControls.FindAction("Brake")
        };

        for (int i = 0; i < rebindButtons.Count; i++)
        {
            int index = i; // To avoid closure problem in the loop
            rebindButtons[i].onClick.AddListener(() => StartCoroutine(StartRebinding(index)));
            UpdateRebindButtonText(index);
        }
    }

    private IEnumerator StartRebinding(int index)
    {
        rebindButtons[index].interactable = false;

        var rebindOperation = playerActions[index].PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse") // Exclude mouse from rebinding
            .OnMatchWaitForAnother(0.1f) // Wait for a short time to allow for additional key presses
            .WithCancelingThrough("<Keyboard>/escape") // Cancel rebinding with Escape key
            .Start();

        yield return new WaitUntil(() => rebindOperation.completed || rebindOperation.canceled);

        RebindComplete(index);
    }

    private void RebindComplete(int index)
    {
        rebindButtons[index].interactable = true;
        UpdateRebindButtonText(index);
    }

    private void UpdateRebindButtonText(int index)
    {
        var binding = playerActions[index].bindings[0];
        var newKey = InputControlPath.ToHumanReadableString(binding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        rebindButtonTexts[index].text = $"{playerActions[index].name}: {newKey}";
    }
}