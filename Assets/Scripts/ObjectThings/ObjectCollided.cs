using UnityEngine;
using UnityEngine.Events;

public class ObjectCollided : MonoBehaviour
{
    //Collision Events
    [Header("Collision Events")]
    public UnityEvent collisionEnterEvent;
    public UnityEvent collisionExitEvent;
    public UnityEvent collisionStayEvent;
    
    //Trigger Events
    [Header("Trigger Events")]
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerExitEvent;
    public UnityEvent triggerStayEvent;
    
    private void OnCollisionEnter(Collision collision)
    {
        collisionEnterEvent.Invoke();
    }

    private void OnCollisionExit(Collision other)
    {
        collisionExitEvent.Invoke();
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        collisionStayEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerEnterEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExitEvent.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        triggerStayEvent.Invoke();
    }
}
