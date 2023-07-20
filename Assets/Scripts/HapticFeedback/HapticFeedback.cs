using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback : MonoBehaviour
{
    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.1f;
    
    public List<GameObject> controllerPrefabs;
    
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice _inputDevice;

    public bool hapticSended;

    private void Start()
    {
        TryInitialize();
    }

    private void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        //Debug.Log("Device: " + devices.Count);
        
        foreach (var item in devices)
        {
            //Debug.Log("Haptic: " + item.name + item.characteristics);
        }
        
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
            //_inputDevice.SendHapticImpulse(cap.numChannels ,defaultAmplitude, defaultDuration);
            _inputDevice.SendHapticImpulse(0 ,defaultAmplitude, defaultDuration);
            //_inputDevice.SendHapticImpulse(1 ,defaultAmplitude, defaultDuration);
            //_inputDevice.SendHapticImpulse(2 ,defaultAmplitude, defaultDuration);
            //_inputDevice.SendHapticImpulse(3 ,defaultAmplitude, defaultDuration);
            //_inputDevice.SendHapticBuffer(cap.numChannels, new byte[1000]);
        }
        
        TryInitialize();

        if (!_inputDevice.isValid)
        {
            //TryInitialize();
        }
    }
}
