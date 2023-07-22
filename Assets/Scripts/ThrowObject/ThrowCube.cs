using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrowCube : MonoBehaviour
{
    public List<GameObject> ignoreObjects;
    public ThrowObjectInteraction throwObjectInteraction;

    private void OnCollisionEnter(Collision other)
    {
        if (ignoreObjects != null && ignoreObjects.Count > 0)
        {
            if (!ignoreObjects.Contains(other.gameObject))
            {
                print("Thrown: " + other.gameObject.name);
                throwObjectInteraction.ObjectThrown(gameObject);
            }
        }
    }

    public void Werfen()
    {
        GetComponent<Rigidbody>().velocity *= 3;
    }
}
