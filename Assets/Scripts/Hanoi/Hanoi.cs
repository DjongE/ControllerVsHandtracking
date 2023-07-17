using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Hanoi : MonoBehaviour
{
    public HanoiHandler hanoiHandler;
    public GameObject underHanoi;

    private RaycastHit _hit;
    private float _damp = 0.01f;

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out _hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 0.01f, Color.magenta);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 0.01f, Color.magenta);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (underHanoi != null)
        {
            if (collision.gameObject.name.Equals(underHanoi.name))
            {
                //Perfect
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 0.01f, out _hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _hit.distance, Color.yellow);
                    hanoiHandler.HanoiPlacedRight();
                }
                
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up) * 0.01f, out _hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * _hit.distance, Color.yellow);
                    hanoiHandler.HanoiPlacedRight();
                }
            }
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (underHanoi != null)
        {
            if (other.gameObject.name.Equals(underHanoi.name))
            {
                hanoiHandler.HanoiRemoved();
            }
        }
    }
}
