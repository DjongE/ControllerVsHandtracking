using System;
using System.Collections.Generic;
using UnityEngine;

public class Taget : MonoBehaviour
{
    public int points = 0;
    public ThrowObjectInteraction throwObjectInteraction;
    public List<GameObject> throwObjects;
    private List<GameObject> _thrownObjects;

    private void Start()
    {
        _thrownObjects = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject throwObject in throwObjects)
        {
            if (other.gameObject.Equals(throwObject))
            {
                throwObjectInteraction.AddPoints(points);
                if (!_thrownObjects.Contains(throwObject))
                {
                    _thrownObjects.Add(throwObject);
                }

                if (_thrownObjects.Count == throwObjects.Count)
                {
                    throwObjectInteraction.ThrowObjectDone();
                }
            }
        }
    }
}
