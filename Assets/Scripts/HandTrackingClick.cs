using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Enables a button for the click-action when handtracking is active.
/// </summary>
public class HandTrackingClick : MonoBehaviour
{
    private HandTrackingObserver[] handTrackingControllers;

    private PressableButton button;
    private ObjectManipulator objectManipulator;

    private void Awake()
    {
        handTrackingControllers = FindObjectsOfType<HandTrackingObserver>();
        objectManipulator = GetComponentInParent<ObjectManipulator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < handTrackingControllers.Length; i++)
        {
            handTrackingControllers[i].articulatedHandShown.AddListener(Show);
            handTrackingControllers[i].articulatedHandHidden.AddListener(Hide);
        }

        button = GetComponentInChildren<PressableButton>();

        button.firstSelectEntered.AddListener(ActivateByButtonClick);
        button.lastSelectExited.AddListener(DeactivateByButtonClick);

        button.gameObject.SetActive(false);

    }

    private void OnDestroy()
    {
        button.firstSelectEntered.RemoveListener(ActivateByButtonClick);
        button.lastSelectExited.RemoveListener(DeactivateByButtonClick);
    }

    private void ActivateByButtonClick(SelectEnterEventArgs args)
    {
        ActivateEventArgs activateEventArgs = new ActivateEventArgs();
        objectManipulator.activated.Invoke(activateEventArgs);
    }

    private void DeactivateByButtonClick(SelectExitEventArgs args)
    {
        DeactivateEventArgs deactivateEventArgs = new DeactivateEventArgs();
        objectManipulator.deactivated.Invoke(deactivateEventArgs);
    }

    private void Show()
    {
        button.gameObject.SetActive(true);
    }

    private void Hide()
    {
        if (HandTrackingObserver.IsHandTrackingActive())
        {
            return;
        }

        button.gameObject.SetActive(false);
    }
}
