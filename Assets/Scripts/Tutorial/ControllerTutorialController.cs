using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using System.Linq;

[System.Serializable]
public struct TutorialStep{
    public ButtonType button;
    public List<InputActionReference> inputActions;
    private List<ButtonVisual> correspondingButtons;

    public void SetCorrespondingButtons(List<ButtonVisual> buttons)
    {
        correspondingButtons = buttons;
    }

    public List<ButtonVisual> GetCorrespondingButtons()
    {
        return correspondingButtons;
    }
}

public class ControllerTutorialController : MonoBehaviour
{
    public InputActionAsset inputActions;

    public UnityEvent<InputAction.CallbackContext> OnTutorialStepDone;
    public UnityEvent OnTutorialComplete;

    [Header("Settings for the tutorial")]

    public bool startTutorialOnStart = true;

    public TutorialStep[] tutorialProzess;

    public ArticulatedHandController[] controllersToExplain;

    private List<ButtonVisual> primaryButtons = new List<ButtonVisual>();
    private List<ButtonVisual> secondaryButtons = new List<ButtonVisual>();
    private List<ButtonVisual> triggerButtons = new List<ButtonVisual>();
    private List<ButtonVisual> gripButtons = new List<ButtonVisual>();
    private List<ButtonVisual> thumbsticks = new List<ButtonVisual>();

    private int currentTutorialStepIndex = 0;

    private List<HandPresence> handVisualsSkript = new List<HandPresence>();

    private void Start()
    {
        foreach(ArticulatedHandController controller in controllersToExplain)
        {
            handVisualsSkript.Add(controller.GetComponentInChildren<HandPresence>());
        }

        handVisualsSkript.ForEach(handpresence => handpresence.OnControllerModelSpawned.AddListener(SetupTutorial));

        OnTutorialStepDone.AddListener(delegate { NextTutorialStep(); });
    }



    public void StartTutorial()
    {
        handVisualsSkript.ForEach(handpresence => handpresence.showController = true);

        // Show help button visuals and wait for help click
        ShowCurrentButton();
    }

    private void SetupTutorial()
    {
        // Setup button visuals
        ButtonVisual[] allVisuals;
        for (int i = 0; i < controllersToExplain.Length; i++)
        {
            allVisuals = controllersToExplain[i].GetComponentsInChildren<ButtonVisual>();

            for(int y = 0; y < allVisuals.Length; y++)
            {
                switch (allVisuals[y].buttonType)
                {
                    case ButtonType.primary:
                        primaryButtons.Add(allVisuals[y]);
                        break;
                    case ButtonType.secondary:
                        secondaryButtons.Add(allVisuals[y]);
                        break;
                    case ButtonType.thumbstick:
                        thumbsticks.Add(allVisuals[y]);
                        break;
                    case ButtonType.grip:
                        gripButtons.Add(allVisuals[y]);
                        break;
                    case ButtonType.trigger:
                        triggerButtons.Add(allVisuals[y]);
                        break;
                }
            }
        }

        // Setup order
        for (int i = 0; i < tutorialProzess.Length; i++)
        {
            switch (tutorialProzess[i].button)
            {
                case ButtonType.primary:
                    tutorialProzess[i].SetCorrespondingButtons(primaryButtons);
                    break;
                case ButtonType.secondary:
                    tutorialProzess[i].SetCorrespondingButtons(secondaryButtons);
                    break;
                case ButtonType.trigger:
                    tutorialProzess[i].SetCorrespondingButtons(triggerButtons);
                    break;
                case ButtonType.grip:
                    tutorialProzess[i].SetCorrespondingButtons(gripButtons);
                    break;
                case ButtonType.thumbstick:
                    tutorialProzess[i].SetCorrespondingButtons(thumbsticks);
                    break;
            }
        }


        if (startTutorialOnStart)
        {
            StartTutorial();
        }
    }

    private void ShowCurrentButton()
    {
        // Setup buttons
        foreach(ButtonVisual button in tutorialProzess[currentTutorialStepIndex].GetCorrespondingButtons())
        {
            button.ShowVisual();

            foreach(InputActionReference inputAction in tutorialProzess[currentTutorialStepIndex].inputActions)
            {
                inputAction.action.performed += button.OnButtonClicked.Invoke;
            }
        }

        // Setup listener for next step
        foreach (InputActionReference inputAction in tutorialProzess[currentTutorialStepIndex].inputActions)
        {
            inputAction.action.performed += OnTutorialStepDone.Invoke;
        }
    }

    private void ClearCurrentButton()
    {
        foreach (ButtonVisual button in tutorialProzess[currentTutorialStepIndex].GetCorrespondingButtons())
        {
            foreach (InputActionReference inputAction in tutorialProzess[currentTutorialStepIndex].inputActions)
            {
                inputAction.action.performed -= button.OnButtonClicked.Invoke;
            }
        }

        foreach (InputActionReference inputAction in tutorialProzess[currentTutorialStepIndex].inputActions)
        {
            inputAction.action.performed -= OnTutorialStepDone.Invoke;
        }
    }

    private void NextTutorialStep()
    {
        // Remove listeners of last step
        ClearCurrentButton();

        // Display next buttons
        currentTutorialStepIndex++;
        if (currentTutorialStepIndex == tutorialProzess.Length)
        {
            // All tutorial steps are completed
            TutorialCompleted();
        }
        else
        {
            ShowCurrentButton();
        }
    }

    private void TutorialCompleted()
    {
        // Hide controllers and show the hand models that are used in the later szenario
        handVisualsSkript.ForEach(handpresence => handpresence.showController = false);

        OnTutorialComplete.Invoke();
    }

}
