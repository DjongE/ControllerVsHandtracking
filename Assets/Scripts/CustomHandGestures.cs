using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class can be used to implement custom hand gestures.
/// </summary>
public class CustomHandGestures : MonoBehaviour
{
    [Header("Triggered when the palm is facing away from the camera in a predefined angle.")]
    public UnityEvent onGestureRayShow;
    public UnityEvent onGestureRayHide;

    [SerializeField]
    private Transform handArmature;

    private ArticulatedHandController articulatedHandController;
    private MRTKHandsAggregatorSubsystem aggregator;

    private IReadOnlyList<HandJointPose> joints;
    private bool isPalmFacingAway;

    private void Awake()
    {
        articulatedHandController = GetComponentInParent<ArticulatedHandController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetHandsAggregatorSubsystem());
    }

    // Update is called once per frame
    void Update()
    {
        // check if the hand is visible and available
        if (aggregator != null && aggregator.TryGetEntireHand(articulatedHandController.HandNode, out joints))
        {
            // check if the palm is facing away and has the correct angle for ray activation
            aggregator.TryGetPalmFacingAway(articulatedHandController.HandNode, out isPalmFacingAway); // rotation of armature: when x < -45

            if(isPalmFacingAway && handArmature.localRotation.eulerAngles.x > 270 && handArmature.localRotation.eulerAngles.x < 320)
            {
                onGestureRayShow.Invoke();
            }

            if (!isPalmFacingAway || (handArmature.localRotation.eulerAngles.x > 320 || handArmature.localRotation.eulerAngles.x < 270))
            {
                onGestureRayHide.Invoke();
            }


        }
    }

    private IEnumerator GetHandsAggregatorSubsystem()
    {
        yield return new WaitUntil(() => XRSubsystemHelpers.GetFirstRunningSubsystem<MRTKHandsAggregatorSubsystem>() != null);
        aggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<MRTKHandsAggregatorSubsystem>();
    }

    private void OnDestroy()
    {
        onGestureRayShow.RemoveAllListeners();
        onGestureRayHide.RemoveAllListeners();
    }
}
