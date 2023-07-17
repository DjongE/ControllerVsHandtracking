using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ThrowObjectInteraction : MonoBehaviour
{
    public int points;
    public AudioSource hitTarget;

    public UnityEvent restartWithHandTrackingEvent;

    public void AddPoints(int point)
    {
        hitTarget.Play();
        points += point;
    }

    public void ThrowObjectDone()
    {
        StartCoroutine(DoneDelay());
    }

    private IEnumerator DoneDelay()
    {
        yield return new WaitForSeconds(5f);
        //Neu Starten mit HandTracking
        restartWithHandTrackingEvent.Invoke();
    }
}
