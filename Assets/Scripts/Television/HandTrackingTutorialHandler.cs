using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrackingTutorialHandler : MonoBehaviour
{
    [Header("Channel 1 - Hand Tracking Tutorial")]
    public GameObject channel1Object;
    public List<GameObject> stepList;
    public GameObject cubeGrabObject;
    public GameObject cubeTouchObject;
    public GameObject startButton;

    [Header("Materials Cube Touched")]
    public Material green;
    private int _actuallyStepIndex;

    [Header("Channel 2 - Dice Interaction")]
    public GameObject channel2;
    private InteractionHandler _interactionHandler;

    public HandsTracker handsTracker;
    
    //Audio
    private AudioSource _notificationSound;

    private void Start()
    {
        _notificationSound = GetComponent<AudioSource>();
        _interactionHandler = GetComponent<InteractionHandler>();
    }

    private void Update()
    {
        if (_actuallyStepIndex == 0)
        {
            if (handsTracker.leftHandIsTracked && handsTracker.rightHandIsTracked)
            {
                NextStepChannel1();
            }
        }
    }
    
    public void StartHandTrackingTutorial()
    {
        channel1Object.SetActive(true);
    }
    
    public void NextStepChannel1()
    {
        stepList[_actuallyStepIndex].SetActive(false);
        _actuallyStepIndex++;
        stepList[_actuallyStepIndex].SetActive(true);
        
        _notificationSound.Play();

        if (_actuallyStepIndex == 1)
        {
            cubeGrabObject.SetActive(true);
        }

        if (_actuallyStepIndex == 3)
        {
            cubeGrabObject.SetActive(false);
            cubeTouchObject.transform.parent.gameObject.SetActive(true);
        }

        if (_actuallyStepIndex == 4)
        {
            cubeTouchObject.transform.parent.gameObject.SetActive(false);
            
            //Open Interaction Menu
            startButton.SetActive(true);
        }
    }
    
    private IEnumerator NextStepWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextStepChannel1();
    }

    //Step 2 - Release cube
    public void RealeaseCube()
    {
        StartCoroutine(NextStepWithDelay(2f));
    }

    //Step 3 - Touch object
    public void ChangeColorToGreen()
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
        startButton.SetActive(false);
        channel1Object.SetActive(false);
        channel2.SetActive(true);
        _interactionHandler.EnableNextInteraction();
        _notificationSound.Play();
    }
}
