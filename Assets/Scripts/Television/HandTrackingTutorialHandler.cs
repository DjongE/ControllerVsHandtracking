using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandTrackingTutorialHandler : MonoBehaviour
{
    [Header("Channel 1 - Hand Tracking Tutorial")]
    public GameObject channel2Object;
    public List<GameObject> stepList;
    public GameObject cubeGrabObject;
    public GameObject cubeTouchObject;

    [Header("Materials Cube Touched")]
    public Material green;
    private int _actuallyStepIndex;
    
    [Header("Channel 3 - Hand Tracking")]
    public GameObject channel3HandTrackingDisplay;
    private InteractionHandler _interactionHandler;
    
    [Header("Hand Tracker & Data Collector")]
    public HandsTracker handsTracker;
    public DataCollector dataCollector;

    [Header("Start Hand Tracking Interaction")]
    public UnityEvent startHandTrackingInteractionEvent;

    [Header("Start Tutorial Event")]
    public UnityEvent startHandTrackingTutorial;
    
    //Notification sound
    private AudioSource _notificationSound;

    private void Start()
    {
        _notificationSound = GetComponent<AudioSource>();
        _interactionHandler = GetComponent<InteractionHandler>();

        if (!_interactionHandler.controller)
        {
            channel2Object.SetActive(true);
            dataCollector.dataName = "HandTracking";
            startHandTrackingTutorial.Invoke();
        }
    }

    private void Update()
    {
        if (_actuallyStepIndex == 0)
        {
            if (handsTracker.leftHandIsTracked && handsTracker.rightHandIsTracked)
            {
                NextStep();
            }
        }
    }
    
    public void StartHandTrackingTutorial()
    {
        channel2Object.SetActive(true);
        dataCollector.dataName = "HandTracking";
        startHandTrackingTutorial.Invoke();
    }
    
    public void NextStep()
    {
        stepList[_actuallyStepIndex].SetActive(false);//Disable the previous television tutorial
        _actuallyStepIndex++;
        stepList[_actuallyStepIndex].SetActive(true);//Enable the next television tutorial
        
        _notificationSound.Play();

        if (_actuallyStepIndex == 1)
        {
            cubeGrabObject.SetActive(true);//Enable grab object tutorial
        }

        if (_actuallyStepIndex == 3)
        {
            cubeGrabObject.SetActive(false);//Disable grab object tutorial
            cubeTouchObject.transform.parent.gameObject.SetActive(true);//Enable touch object tutorial
        }

        if (_actuallyStepIndex == 4)
        {
            cubeTouchObject.transform.parent.gameObject.SetActive(false);
            HandTrackingTutorialDone();
        }
    }

    private IEnumerator StartInteractions()
    {
        yield return new WaitForSeconds(3f);
        startHandTrackingInteractionEvent.Invoke();
    }
    
    private IEnumerator NextStepWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextStep();
    }

    //Step 2 - Release cube
    public void RealeaseCube()//Grab tutorial
    {
        StartCoroutine(NextStepWithDelay(2f));
    }

    //Step 3 - Touch object
    public void ChangeColorToGreen()//Touch tutorial
    {
        cubeTouchObject.GetComponent<MeshRenderer>().material = green;
        StartCoroutine(NextStepWithDelay(2f));
    }

    //The Hand Tracking Tutorial is finished
    public void HandTrackingTutorialDone()
    {
        StartCoroutine(DelayDone());
    }

    private IEnumerator DelayDone()
    {
        yield return new WaitForSeconds(2f);
        channel2Object.SetActive(false);
        channel3HandTrackingDisplay.SetActive(true);
        _interactionHandler.NextInteraction();
        _notificationSound.Play();
    }
}
