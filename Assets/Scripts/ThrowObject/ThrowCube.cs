using System.Collections.Generic;
using UnityEngine;

public class ThrowCube : MonoBehaviour
{
    [Header("Ignore Collider")]
    public List<GameObject> ignoreObjects;
    
    [Header("Throw Object Interaction")]
    public ThrowObjectInteraction throwObjectInteraction;

    private void OnCollisionEnter(Collision other)
    {
        if (ignoreObjects != null && ignoreObjects.Count > 0)
        {
            if (!ignoreObjects.Contains(other.gameObject))
            {
                throwObjectInteraction.ObjectThrown(gameObject);
            }
        }
    }

    public void Werfen()
    {
        GetComponent<Rigidbody>().velocity *= 3;
    }
}
