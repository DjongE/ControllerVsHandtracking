using System.Collections;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Header("Interaction Handler")]
    public InteractionHandler interactionHandler;
    public InteractionTimer timer;
    
    [Header("Non Haptic Button")]
    public PressableButton nonHapticButtonLightOn;
    public PressableButton nonHapticButtonLightOff;

    [Header("Non Haptic Light Bulb")] 
    public GameObject nonHapticLight;
    
    [Header("Haptic Button")]
    public PressableButton hapticButtonLightOn;
    public PressableButton hapticButtonLightOff;
    
    [Header("Haptic Light Bulb")] 
    public GameObject hapticLight;

    private int _switchedCounter;

    private Coroutine _done;

    private void Start()
    {
        nonHapticButtonLightOff.StartPushPlane = -0.008356876f;
        nonHapticButtonLightOff.GetComponent<BasicPressableButtonVisuals>().MovingVisuals.localPosition =
            new Vector3(0, 0, -0.008356876f);//Set one of the switches into the "pressed" position
        
        hapticButtonLightOff.StartPushPlane = -0.008356876f;
        hapticButtonLightOff.GetComponent<BasicPressableButtonVisuals>().MovingVisuals.localPosition =
            new Vector3(0, 0, -0.008356876f);//Set one of the switches into the "pressed" position
    }

    public void LightSwitchDone()
    {
        if (_switchedCounter >= 6)
        {
            if(_done == null)
                _done = StartCoroutine(DoneDelay());
        }
    }

    //Lightswitch interaction is done
    private IEnumerator DoneDelay()
    {
        timer.StopTimer();
        interactionHandler.AddInteractionData("LightSwitch");
        yield return new WaitForSeconds(2f);
        interactionHandler.NextInteraction();
    }

    public void ClickLightOnNonHaptic()
    {
        nonHapticButtonLightOff.StartPushPlane = -0.01747753f;//Released switch position z
        nonHapticButtonLightOn.StartPushPlane = -0.008356876f;//Pressed switch position z
        
        nonHapticLight.SetActive(true);

        _switchedCounter++;
        
        if(!timer.TimerStarted())
            timer.StartTimer();
        
        LightSwitchDone();
    }
    
    public void ClickLightOffNonHaptic()
    {
        nonHapticButtonLightOn.StartPushPlane = -0.01747753f;
        nonHapticButtonLightOff.StartPushPlane = -0.008356876f;
        
        nonHapticLight.SetActive(false);
    }
    
    public void ClickLightOnHaptic()
    {
        hapticButtonLightOff.StartPushPlane = -0.01747753f;
        hapticButtonLightOn.StartPushPlane = -0.008356876f;
        
        hapticLight.SetActive(true);

        _switchedCounter++;

        if(!timer.TimerStarted())
            timer.StartTimer();
        
        LightSwitchDone();
    }
    
    public void ClickLightOffHaptic()
    {
        hapticButtonLightOn.StartPushPlane = -0.01747753f;
        hapticButtonLightOff.StartPushPlane = -0.008356876f;
        
        hapticLight.SetActive(false);
    }
}
