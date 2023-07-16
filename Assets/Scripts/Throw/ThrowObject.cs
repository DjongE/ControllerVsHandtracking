using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.XR;

public class ThrowObject : MonoBehaviour
{
    public Transform head;
    
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice _leftControllerDevice;

    private void Start()
    {
        TryInit();
    }

    private void Update()
    {
        if(!_leftControllerDevice.isValid)
            TryInit();
    }

    private void TryInit()
    {
        controllerCharacteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left;
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            _leftControllerDevice = devices[0];
        }
    }

    public void Drop()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        if (_leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 controllerVel))
        {
            Debug.Log("Head Rotation: " + head.rotation.eulerAngles);
            rb.velocity = head.rotation * controllerVel;
        }
        
        if (_leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out Vector3 controllerAngVel))
        {
            //Debug.Log("LeftHandVelocity: " + LeftControllerVelocity);
            rb.angularVelocity = controllerAngVel;
        }
    }
}
