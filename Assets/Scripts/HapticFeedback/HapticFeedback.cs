using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.XR;

public class HapticFeedback : MonoBehaviour
{
    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 5f;

    public ArticulatedHandController articulatedHandController;
    
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

        foreach (var item in devices)
        {
            Debug.Log("Haptic: " + item.name + item.characteristics);
        }
        
        if (devices.Count > 0)
        {
            if(_inputDevice.TryGetHapticCapabilities(out HapticCapabilities cap))
            {
                _inputDevice.SendHapticImpulse(cap.numChannels ,defaultAmplitude, defaultDuration);
            }
            _inputDevice = devices[0];
        }

        if ((controllerCharacteristics & InputDeviceCharacteristics.TrackedDevice) != 0)
        {
            Debug.Log(InputDeviceCharacteristics.TrackedDevice);
        }
    }

    public void SendHaptics()
    {
        /*bool a = inpusDevice.SendHapticImpulse(0 ,defaultAmplitude, defaultDuration);
        bool b = inpusDevice.SendHapticImpulse(1 ,defaultAmplitude, defaultDuration);
        bool c = inpusDevice.SendHapticImpulse(2 ,defaultAmplitude, defaultDuration);
        bool d = inpusDevice.SendHapticImpulse(3 ,defaultAmplitude, defaultDuration);*/

        /*if (inpusDevice.TryGetHapticCapabilities(out HapticCapabilities cap))
        {
            print("bbbbbbbbbbbbbb");
            inpusDevice.SendHapticImpulse(cap.numChannels ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticBuffer(cap.numChannels, new byte[1000]);
        }*/

        //Debug.Log(a + " " + b + " " + c + " " + d);

        hapticSended = true;
    }

    public void NeinHaptics()
    {
        hapticSended = false;
    }

    private void Update()
    {
        if (articulatedHandController.selectActionValue.action.ReadValue<float>() > 0.0f )
        {
            //if(inpusDevice.TryGetHapticCapabilities(out HapticCapabilities cap2))
            //{
                /*Debug.Log("Haptic: " + cap.numChannels + " " + cap.supportsImpulse);
                inpusDevice.SendHapticImpulse(cap.numChannels ,defaultAmplitude, defaultDuration);
                inpusDevice.SendHapticImpulse(0 ,defaultAmplitude, defaultDuration);
                inpusDevice.SendHapticImpulse(1 ,defaultAmplitude, defaultDuration);
                inpusDevice.SendHapticImpulse(2 ,defaultAmplitude, defaultDuration);
                inpusDevice.SendHapticImpulse(3 ,defaultAmplitude, defaultDuration);
                inpusDevice.SendHapticBuffer(cap.numChannels, new byte[1000]);*/
            //}
        }
        
        if(_inputDevice.TryGetHapticCapabilities(out HapticCapabilities cap) && hapticSended)
        {
            Debug.Log("Haptic: " + cap.numChannels + " " + cap.supportsImpulse);
            _inputDevice.SendHapticImpulse(cap.numChannels ,defaultAmplitude, defaultDuration);
            _inputDevice.SendHapticImpulse(0 ,defaultAmplitude, defaultDuration);
            _inputDevice.SendHapticImpulse(1 ,defaultAmplitude, defaultDuration);
            _inputDevice.SendHapticImpulse(2 ,defaultAmplitude, defaultDuration);
            _inputDevice.SendHapticImpulse(3 ,defaultAmplitude, defaultDuration);
            _inputDevice.SendHapticBuffer(cap.numChannels, new byte[1000]);
        }

        if (!_inputDevice.isValid)
        {
            TryInitialize();
        }
    }
}
