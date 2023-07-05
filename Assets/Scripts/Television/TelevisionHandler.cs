using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelevisionHandler : MonoBehaviour
{
    [Header("Channel 1 - Hand Tracking Tutorial")]
    public GameObject channel1Object;
    public List<GameObject> stepList;
    private int _actuallyStepIndex;

    public HandsTracker handsTracker;
    
    //Audio
    private AudioSource _notificationSound;

    private void Start()
    {
        _notificationSound = GetComponent<AudioSource>();
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
    }
}
