using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ThrowObjectInteraction : MonoBehaviour
{
    public InteractionHandler interactionHandler;
    public int points;
    public TextMeshPro pointsText;
    
    public AudioSource hitTarget;

    public UnityEvent restartWithHandTrackingEvent;
    public InteractionTimer timer;

    private Coroutine _done;
    private int _objectThrownCounter;
    private List<GameObject> _objectsThrown;

    private void Start()
    {
        if (_objectsThrown == null)
            _objectsThrown = new List<GameObject>();
    }

    public void StartThrowObject()
    {
        if(!timer.TimerStarted())
            timer.StartTimer();
    }
    
    public void AddPoints(int point)
    {
        hitTarget.Play();
        points += point;
        pointsText.text = ("Punkte: " + points);
    }

    public void ObjectThrown(GameObject thrownObject)
    {
        if (!_objectsThrown.Contains(thrownObject))
        {
            _objectsThrown.Add(thrownObject);
        }

        if (_objectsThrown.Count >= 5)
        {
            ThrowObjectDone();
        }
    }

    public void ThrowObjectDone()
    {
        if(_done == null)
            _done = StartCoroutine(DoneDelay());
    }

    private IEnumerator DoneDelay()
    {
        timer.StopTimer();
        interactionHandler.AddInteractionData("ThrowObject");
        yield return new WaitForSeconds(2f);
        interactionHandler.NextInteraction();
    }
}
