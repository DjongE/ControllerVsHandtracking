using System;
using System.Collections;
using System.Collections.Generic;
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

    private float _startPushPlane = -0.01747753f;
    private float _endPushPlane = -0.008356876f;

    private int _switchedCounter;

    private Coroutine _done;

    private void Start()
    {
        nonHapticButtonLightOff.StartPushPlane = -0.008356876f;
        nonHapticButtonLightOff.GetComponent<BasicPressableButtonVisuals>().MovingVisuals.localPosition =
            new Vector3(0, 0, -0.008356876f);
        
        hapticButtonLightOff.StartPushPlane = -0.008356876f;
        hapticButtonLightOff.GetComponent<BasicPressableButtonVisuals>().MovingVisuals.localPosition =
            new Vector3(0, 0, -0.008356876f);
    }

    public void LightSwitchDone()
    {
        if (_switchedCounter >= 6)
        {
            timer.StopTimer();
            if(_done == null)
                _done = StartCoroutine(DoneDelay());
        }
    }

    private IEnumerator DoneDelay()
    {
        yield return new WaitForSeconds(2f);
        interactionHandler.EnableNextInteraction();
    }

    public void ClickLightOnNonHaptic()
    {
        nonHapticButtonLightOff.StartPushPlane = -0.01747753f;
        nonHapticButtonLightOn.StartPushPlane = -0.008356876f;
        
        nonHapticLight.SetActive(true);

        _switchedCounter++;
        LightSwitchDone();
        
        if(timer.TimerStopped())
            timer.StartTimer();
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
        LightSwitchDone();
        
        if(timer.TimerStopped())
            timer.StartTimer();
    }
    
    public void ClickLightOffHaptic()
    {
        hapticButtonLightOn.StartPushPlane = -0.01747753f;
        hapticButtonLightOff.StartPushPlane = -0.008356876f;
        
        hapticLight.SetActive(false);
    }
}
