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

    private int _objectPlacedCounter;

    public void StartGrabAndPlaceTimer()
    {
        if (timer.TimerStopped())
        {
            timer.StartTimer();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_objectPlacedCounter < 2)
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
    }

    private void CheckAllCubesPlaced()
    {
        if(_object1Placed && _object2Placed)
        {
            //Interaction done
            timer.StopTimer();
            interactionHandler.GetComponent<AudioSource>().Play();
            StartCoroutine(GrabPlaceFinished());
        }
    }

    private IEnumerator GrabPlaceFinished()
    {
        yield return new WaitForSeconds(2f);
        interactionHandler.EnableNextInteraction();
    }
}
