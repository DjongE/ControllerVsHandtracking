using System.Collections;
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

    [Header("Interaction Timer")]
    public InteractionTimer timer;
    private Coroutine _done;

    public void StartGrabAndPlaceTimer()
    {
        if (!timer.TimerStarted())
            timer.StartTimer();
    }
    
    //Checks if the dice have been placed in the orange basket
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

    //Start ord stop a Coroutine when both cubes are placed in the basket
    private void CheckAllCubesPlaced()
    {
        if(_object1Placed && _object2Placed)
        {
            //Interaction done
            interactionHandler.GetComponent<AudioSource>().Play();
            _done = StartCoroutine(GrabPlaceFinished());
        }
        
        if (!_object1Placed || !_object2Placed)
        {
            if (_done != null)
                StopCoroutine(_done);
        }
    }

    //Executed when both cubes are placed in the basket
    private IEnumerator GrabPlaceFinished()
    {
        yield return new WaitForSeconds(2f);
        timer.StopTimer();
        timer.SetTimeInSeconds(timer.GetTimeInSeconds() - 2f);
        interactionHandler.AddInteractionData("GrabAndPlace");
        interactionHandler.NextInteraction();
    }
}
