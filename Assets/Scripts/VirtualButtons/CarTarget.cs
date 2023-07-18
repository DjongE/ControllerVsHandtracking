using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTarget : MonoBehaviour
{
    public AudioSource arrivedTarget;
    public VirtualButtons virtualButtons;
    public GameObject car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(car))
        {
            virtualButtons.VirtualButtonsIsDone();
            arrivedTarget.Play();
        }
    }
}
