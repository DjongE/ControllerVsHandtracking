using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Checks if hand tracking is currently active
/// </summary>
public class HandTrackingObserver : MonoBehaviour
{
    [Tooltip("Called when the corresponding hand appears. NOTE: does not determine if hand tracking is active in general. For this, use IsHandtrackingActive function.")]
    public UnityEvent articulatedHandShown = new UnityEvent();
    [Tooltip("Called when the corresponding hand disappears. NOTE: does not determine if hand tracking is active in general. For this, use IsHandtrackingActive function.")]
    public UnityEvent articulatedHandHidden = new UnityEvent();

    private bool articulatedHandCurrentlyShown = false;

    private ArticulatedHandController articulatedHandController;
    private GameObject articulatedHandModel;
    private SkinnedMeshRenderer articulatedHandRenderer;
    private bool isArticulatedHandModelSetup = false;

    private static HandTrackingObserver[] handTrackingControllers;

    private void Awake()
    {
        articulatedHandController = GetComponent<ArticulatedHandController>();
        handTrackingControllers = FindObjectsOfType<HandTrackingObserver>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isArticulatedHandModelSetup)
        {
            if (articulatedHandController.model)
            {
                articulatedHandModel = articulatedHandController.model.gameObject;
                articulatedHandRenderer = articulatedHandModel.GetComponentInChildren<SkinnedMeshRenderer>();
                isArticulatedHandModelSetup = true;
                Debug.LogWarning("setup articulated hands - might take a while");
            }
        }

        if (!articulatedHandCurrentlyShown && articulatedHandRenderer.enabled)
        {
            // The hands were not visible last frame, but turned visible this frame
            articulatedHandCurrentlyShown = true;
            articulatedHandShown.Invoke();
        }

        if(articulatedHandCurrentlyShown && !articulatedHandRenderer.enabled)
        {
            // The hands were visible last frame, but turned invisible this frame
            articulatedHandCurrentlyShown = false;
            articulatedHandHidden.Invoke();
        }
    }

    /// <summary>
    /// Checks if handtracking is currently active.
    /// </summary>
    /// <returns>true, when at least one hand is currently shown.</returns>
    public static bool IsHandTrackingActive()
    {
        for(int i = 0; i < handTrackingControllers.Length; i++)
        {
            if (handTrackingControllers[i].articulatedHandRenderer && handTrackingControllers[i].articulatedHandRenderer.enabled)
            {
                // at least one hand is shown
                return true;
            }
        }
        return false;
    }
}
