using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using UnityEngine.Events;

public class HandsTracker : MonoBehaviour
{
    [Header("OpenXR Hands")]
    public ArticulatedHandController leftHandController;
    private SkinnedMeshRenderer _leftHandModel;
    public ArticulatedHandController rightHandController;
    private SkinnedMeshRenderer _rightHandModel;

    [Header("Hands Tracked Events")]
    public UnityEvent leftHandIsTrackedEvent;
    public UnityEvent rightHandIsTrackedEvent;
    
    [Header("Hand Tracking State")]
    public bool leftHandIsTracked;
    public bool rightHandIsTracked;
    

    private void Update()
    {
        HandsTracked();
    }

    private void HandsTracked()
    {
        _leftHandModel = leftHandController.model.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        _rightHandModel = rightHandController.model.GetChild(0).GetComponent<SkinnedMeshRenderer>();

        if (_leftHandModel.enabled)
        {
            leftHandIsTracked = true;
            leftHandIsTrackedEvent.Invoke();
        }
        else
        {
            leftHandIsTracked = false;
        }

        if (_rightHandModel.enabled)
        {
            rightHandIsTracked = true;
            rightHandIsTrackedEvent.Invoke();
        }
        else
        {
            rightHandIsTracked = false;
        }
    }
}
