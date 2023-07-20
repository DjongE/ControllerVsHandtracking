using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.OpenXR;
using UnityEngine;
using UnityEngine.Events;

public class ControllerTutorial : MonoBehaviour
{
    [Header("Channel 1 - Controller Tutorial")]
    public GameObject channel1Object;
    public List<GameObject> stepList;
    public GameObject cubeGrabObject;
    public GameObject cubeTouchObject;

    [Header("Materials Cube Touched")]
    public Material green;
    private int _actuallyStepIndex;
    
    [Header("Channel 3 - Interaction")]
    public GameObject channel3InteractionDisplay;
    private InteractionHandler _interactionHandler;

    [Header("Controller Tracker & Data Collector")]
    public HandPresence leftControllerTracker;
    public HandPresence rightControllerTracker;
    public DataCollector dataCollector;
    
    [Header("Start Interaction")]
    public UnityEvent startInteractionsEvent;

    [Header("Start Controller Tutorial Event")]
    public UnityEvent startControllerTutorialEvent;
    
    //Audio
    private AudioSource _notificationSound;

    private void Start()
    {
        _notificationSound = GetComponent<AudioSource>();
        _interactionHandler = GetComponent<InteractionHandler>();
        
        if (_interactionHandler.controller)
        {
            channel1Object.SetActive(true);
            startControllerTutorialEvent.Invoke();
            dataCollector.dataName = "Controller";
        }
    }

    private void LateUpdate()
    {
        if (_actuallyStepIndex == 0)
        {
            if(!leftControllerTracker.showController && !rightControllerTracker.showController)
                NextStep();
        }
    }

    public void NextStep()
    {
        stepList[_actuallyStepIndex].SetActive(false);
        _actuallyStepIndex++;
        stepList[_actuallyStepIndex].SetActive(true);
        
        _notificationSound.Play();
        
        if(_actuallyStepIndex == 1)
            cubeGrabObject.SetActive(true);

        if (_actuallyStepIndex == 3)
        {
            cubeGrabObject.SetActive(false);
            cubeTouchObject.transform.parent.gameObject.SetActive(true);
        }

        if (_actuallyStepIndex == 4)
        {
            cubeTouchObject.transform.parent.gameObject.SetActive(false);
            ControllerTutorialDone();
        }
    }

    private IEnumerator NextStepWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextStep();
    }

    public void ReleaseCube()
    {
        StartCoroutine(NextStepWithDelay(2f));
    }

    public void ChangeColorToGreen()
    {
        cubeTouchObject.GetComponent<MeshRenderer>().material = green;
        StartCoroutine(NextStepWithDelay(2f));
    }

    public void ControllerTutorialDone()
    {
        StartCoroutine(DelayDone());
    }

    private IEnumerator DelayDone()
    {
        yield return new WaitForSeconds(2f);
        channel1Object.SetActive(false);
        channel3InteractionDisplay.SetActive(true);
        _interactionHandler.NextInteraction();
        _notificationSound.Play();
    }
}
