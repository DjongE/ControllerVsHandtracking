using System;
using System.Collections.Generic;
using Microsoft.MixedReality.OpenXR;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.PlayerLoop;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback : MonoBehaviour
{
    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 5f;

    public ArticulatedHandController articulatedHandController;
    
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice inpusDevice;

    public bool hapticSended;

    private void Start()
    {
        TryInitialize();
        Debug.Log("HALLO!");
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
            Debug.Log("Geräte erkannt! ");
            if(inpusDevice.TryGetHapticCapabilities(out HapticCapabilities cap))
            {
                Debug.Log("Haptic: " + cap.numChannels);
                inpusDevice.SendHapticImpulse(cap.numChannels ,defaultAmplitude, defaultDuration);
            }
            inpusDevice = devices[0];
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
        
        if(inpusDevice.TryGetHapticCapabilities(out HapticCapabilities cap) && hapticSended)
        {
            Debug.Log("Haptic: " + cap.numChannels + " " + cap.supportsImpulse);
            inpusDevice.SendHapticImpulse(cap.numChannels ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticImpulse(0 ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticImpulse(1 ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticImpulse(2 ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticImpulse(3 ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticBuffer(cap.numChannels, new byte[1000]);
        }
        
        if(inpusDevice.TryGetHapticCapabilities(out HapticCapabilities cap2))
        {
            Debug.Log("Haptic: " + cap2.numChannels + " " + cap2.supportsImpulse);
            inpusDevice.SendHapticImpulse(cap2.numChannels ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticImpulse(0 ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticImpulse(1 ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticImpulse(2 ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticImpulse(3 ,defaultAmplitude, defaultDuration);
            inpusDevice.SendHapticBuffer(cap2.numChannels, new byte[1000]);
        }
        
        if (!inpusDevice.isValid)
        {
            TryInitialize();
        }
    }
}
