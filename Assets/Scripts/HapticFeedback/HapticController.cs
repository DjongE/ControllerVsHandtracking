using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine;

public class HapticController : MonoBehaviour
{
    [Header("Haptic Feedback Amplitude & Duration")]
    public float amplitude = 1f;
    public float duration = 1f;
    
    public MRTKBaseInteractable _baseInteractable;
    private GameObject _actuallyController;
    
    private void Start()
    {
        //_baseInteractable = GetComponent<MRTKBaseInteractable>();
    }

    public void SendHapticToController()
    {
        foreach (var test in _baseInteractable.interactorsSelecting)
        {
            foreach (GameObject controller in test.transform.GetComponent<InteractionDetector>().GetControllers())
            {
                _actuallyController = controller;
                controller.GetComponentInChildren<HapticFeedback>().SendHaptics(amplitude, duration);
            }
        }
    }

    public void SendHapticToControllerWithTime()
    {
        foreach (var test in _baseInteractable.interactorsHovering)
        {
            Debug.Log("Test: " + test.transform.parent);

            _actuallyController = test.transform.parent.gameObject;
            test.transform.parent.GetComponentInChildren<HapticFeedback>().SendHapticsWithTime(amplitude, duration);
        }
    }

    public void StopHaptic()
    {
        if(_actuallyController != null)
            _actuallyController.GetComponentInChildren<HapticFeedback>().StopHaptics();
    }
}
