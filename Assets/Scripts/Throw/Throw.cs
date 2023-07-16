using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Throw : MonoBehaviour
{
    public Rigidbody objectRigi;

    private Vector3 objectCenterOfMass;

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice _leftControllerDevice;
    private InputDevice _rightControllerDevice;
    Vector3 LeftControllerVelocity;
    private Vector3 AngularLeftControllerVelocity;
    private Vector3 leftPos;
    Vector3 RightControllerVelocity;
    
    private void Start()
    {
        objectRigi = GetComponent<Rigidbody>();
        objectCenterOfMass = objectRigi.centerOfMass;
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

    private void Update()
    {
        if (_leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity))
        {
            //Debug.Log("LeftHandVelocity: " + LeftControllerVelocity);
        }
        
        _rightControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);
        
        if (!_leftControllerDevice.isValid)
        {
            TryInit();
        }
    }
    
    public bool GetThrownObjectVelAngVel(Vector3 throwingControllerPos, Vector3 throwingControllerVelocity, Vector3 throwingControllerAngularVelocity,
        Vector3 thrownObjectCenterOfMass, out Vector3 objectVelocity, out Vector3 objectAngularVelocity)
    {
        Vector3 radialVec = thrownObjectCenterOfMass - throwingControllerPos;

        objectVelocity = throwingControllerVelocity + Vector3.Cross(throwingControllerAngularVelocity, radialVec);
        objectAngularVelocity = throwingControllerAngularVelocity;
        return true;
    }

    public void GrabEnd()
    {
        Vector3 velocity, angularVelocity;
        if (_leftControllerDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPos))
        {
            if (_leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 controllerVel))
            {
                if (_leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 controllerAngVel))
                {
                    if (GetThrownObjectVelAngVel(controllerPos, controllerVel, controllerAngVel, objectCenterOfMass,
                            out velocity, out angularVelocity))
                    {
                        Rigidbody rb = GetComponent<Rigidbody>();
                        rb.angularVelocity = angularVelocity;
                        rb.velocity = velocity;
                        rb.isKinematic = false;
                        Debug.Log("WERFEN!");
                    }
                }
            }
        }
    }
    
    /*public void GrabEnd()
    {
        if (_leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity))
        {
            Debug.Log("LeftHandVelocity: " + LeftControllerVelocity);
        }


        if (_leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceAngularVelocity,
                out AngularLeftControllerVelocity))
        {
            Debug.Log("LeftAngularHandVelocity: " + AngularLeftControllerVelocity);
        }
        
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 fullThrowVelocity = default;
        if (_leftControllerDevice.subsystem.)
        {
            fullThrowVelocity = LeftControllerVelocity + Vector3.Cross(AngularLeftControllerVelocity, transform.position - leftPos);
        }
        
        rb.isKinematic = false;
        rb.velocity = fullThrowVelocity;
        rb.angularVelocity = AngularLeftControllerVelocity;
    }*/
}
