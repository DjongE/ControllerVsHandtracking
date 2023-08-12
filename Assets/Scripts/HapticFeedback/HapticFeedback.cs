using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HapticFeedback : MonoBehaviour
{
    [Header("Default Amplitude and Duration")]
    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.1f;
    
    [Header("Setting for the controller characteristics")]
    public InputDeviceCharacteristics controllerCharacteristics;//E.g. Controller, Left
    private InputDevice _inputDevice;

    [Header("Information - Haptic Feedback sended")]
    public bool hapticSended;

    private void Start()
    {
        TryInitialize();
    }

    private void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);//Try to get the correct controller with the characteristics

        if (devices.Count > 0)
        {
            _inputDevice = devices[0];
        }

        if ((controllerCharacteristics & InputDeviceCharacteristics.TrackedDevice) != 0)
        {
            Debug.Log(InputDeviceCharacteristics.TrackedDevice);
        }
    }

    public void SendHaptics(float amplitude, float duration)
    {
        defaultAmplitude = amplitude;
        defaultDuration = duration;
        hapticSended = true;
    }

    public void SendHapticsWithTime(float amplitude, float duration)
    {
        _inputDevice.SendHapticImpulse(0 ,amplitude, duration);
    }

    public void StopHaptics()
    {
        hapticSended = false;
    }

    private void Update()
    {
        if(_inputDevice.TryGetHapticCapabilities(out HapticCapabilities cap) && hapticSended)
        {
            _inputDevice.SendHapticImpulse(0 ,defaultAmplitude, defaultDuration);//Trigger the Haptic Feedback
        }
        
        TryInitialize();
    }
}
