using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndPlace : MonoBehaviour
{
    [Header("Grab Place Objects")]
    public GameObject grabPlace1;
    private bool _object1Placed;
    
    public GameObject grabPlace2;
    private bool _object2Placed;

    [Header("Interaction Handler")]
    public InteractionHandler interactionHandler;

    public InteractionTimer timer;
    private Coroutine _done;

    public void StartGrabAndPlaceTimer()
    {
        if (!timer.TimerStarted())
            timer.StartTimer();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(grabPlace1))
        {
            _object1Placed = true;
            interactionHandler.GetComponent<AudioSource>().Play();
            CheckAllCubesPlaced();
        }

        if (other.gameObject.Equals(grabPlace2))
        {
            _object2Placed = true;
            interactionHandler.GetComponent<AudioSource>().Play();
            CheckAllCubesPlaced();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(grabPlace1))
        {
            _object1Placed = false;
            CheckAllCubesPlaced();
        }

        if (other.gameObject.Equals(grabPlace2))
        {
            _object2Placed = false;
            CheckAllCubesPlaced();
        }
    }

    private void CheckAllCubesPlaced()
    {
        Debug.Log("GrabAndPlace: " + _object1Placed + " : " + _object2Placed);
        if(_object1Placed && _object2Placed)
        {
            Debug.Log("GrabAndPlace2: " + "Start Coroutine");
            //Interaction done
            interactionHandler.GetComponent<AudioSource>().Play();
            _done = StartCoroutine(GrabPlaceFinished());
        }
        Debug.Log("GrabAndPlace2: " + _object1Placed + " : " + _object2Placed);
        if (!_object1Placed || !_object2Placed)
        {
            Debug.Log("GrabAndPlace2: " + "Stop Coroutine");
            if (_done != null)
                StopCoroutine(_done);
        }
    }

    private IEnumerator GrabPlaceFinished()
    {
        yield return new WaitForSeconds(2f);
        timer.StopTimer();
        timer.SetTimeInSeconds(timer.GetTimeInSeconds() - 2f);
        interactionHandler.AddInteractionData("GrabAndPlace");
        interactionHandler.NextInteraction();
    }
}
