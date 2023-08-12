using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class HapticController : MonoBehaviour
{
    [Header("Haptic Feedback Amplitude & Duration")]
    public float amplitude = 1f;
    public float duration = 1f;
    
    [Header("MRTKBaseInteractable")]
    public MRTKBaseInteractable baseInteractable;
    private GameObject _actuallyController;

    public void SendHapticToController()
    {
        foreach (var test in baseInteractable.interactorsSelecting)
        {
            foreach (GameObject controller in test.transform.GetComponent<InteractionDetector>().GetControllers())
            {
                _actuallyController = controller;//Get the correct hand, with which the interaction was performed
                controller.GetComponentInChildren<HapticFeedback>().SendHaptics(amplitude, duration);//Send haptics to the correct controller
            }
        }
    }

    public void SendHapticToControllerWithTime()
    {
        foreach (var test in baseInteractable.interactorsHovering)
        {
            _actuallyController = test.transform.parent.gameObject;//Get the correct hand, with which the interaction was performed
            test.transform.parent.GetComponentInChildren<HapticFeedback>().SendHapticsWithTime(amplitude, duration);//Send haptics to the correct controller
        }
    }

    public void StopHaptic()
    {
        if(_actuallyController != null)
            _actuallyController.GetComponentInChildren<HapticFeedback>().StopHaptics();
    }
}
