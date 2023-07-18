using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ThrowObjectInteraction : MonoBehaviour
{
    public int points;
    public AudioSource hitTarget;

    public UnityEvent restartWithHandTrackingEvent;

    public InteractionTimer timer;

    private Coroutine _done;

    public void StartThrowObject()
    {
        if(timer.TimerStopped())
            timer.StartTimer();
    }
    
    public void AddPoints(int point)
    {
        hitTarget.Play();
        points += point;
    }

    public void ThrowObjectDone()
    {
        timer.StopTimer();
        
        if(_done == null)
            _done = StartCoroutine(DoneDelay());
    }

    private IEnumerator DoneDelay()
    {
        yield return new WaitForSeconds(5f);
        //Neu Starten mit HandTracking
        restartWithHandTrackingEvent.Invoke();
    }
}
