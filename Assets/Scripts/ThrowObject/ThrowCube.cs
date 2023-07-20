using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCube : MonoBehaviour
{
    public List<GameObject> ignoreObjects;
    public ThrowObjectInteraction throwObjectInteraction;

    private void Start()
    {
        //if (ignoreObjects == null)
            //ignoreObjects = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collided This: " + gameObject.name);
        if (ignoreObjects != null && ignoreObjects.Count > 0)
        {
            if (!ignoreObjects.Contains(other.gameObject))
            {
                throwObjectInteraction.ObjectThrown(gameObject);
                //Debug.Log("Collided With: " + other.gameObject.name);
            }
        }
    }
}
