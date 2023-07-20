using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionHandler : MonoBehaviour
{
    [Header("Reverse Interaction")]
    public bool reverse;
    public bool controller;
    private string _inputName;

    [Header("Start Controller Event")] public UnityEvent startControllerEvent;
    [Header("Start Hand Tracking Event")] public UnityEvent startHandTrackingEvent;

    [Header("Interactions Controller")]
    public List<GameObject> interactions;
    public InteractionTimer interactionTimer;
    
    [Header("Television Interaction Objects")]
    public List<GameObject> tvInteractionsDisplay;
    
    private int _countGrab;
    private int _countRelease;
    private int _countTouch;

    [Header("Television Done")]
    public GameObject doneDisplay;
    
    [Header("Data Collector")]
    public DataCollector dataCollector;

    private int _actuallyInteraction;
    private AudioSource _doneSound;

    private void Start()
    {
        _doneSound = GetComponent<AudioSource>();

        if (reverse)
            _actuallyInteraction = interactions.Count-1;
        else
            _actuallyInteraction = 0;
        
        print("Reverse: " + _actuallyInteraction);

        if (controller)
        {
            _inputName = "Controller";
            startControllerEvent.Invoke();
        }
        else
        {
            _inputName = "HandTracking";
            startHandTrackingEvent.Invoke();
        }
    }

    public void NextInteraction()
    {
        if (reverse)
        {
            EnableReverseInteraction();
            Debug.Log("Next");
        }
        else
        {
            Debug.Log("Next2: " + _actuallyInteraction);
            EnableNextInteraction();
        }
    }

    public void EnableNextInteraction()
    {
        _doneSound.Play();
        
        if (_actuallyInteraction > 5)
        {
            //Done - Save Data
            dataCollector.GenerateJSON(_inputName);
            doneDisplay.SetActive(true);
        }
        
        //Disable previous interaction
        if (_actuallyInteraction > 0)
        {
            interactions[_actuallyInteraction - 1].SetActive(false);
            tvInteractionsDisplay[_actuallyInteraction - 1].SetActive(false);
        }

        if (_actuallyInteraction < 6)
        {
            //Enable Interaction
            interactions[_actuallyInteraction].SetActive(true);
            tvInteractionsDisplay[_actuallyInteraction].SetActive(true);
        }
        
        _actuallyInteraction++;
    }
    
    public void EnableReverseInteraction()
    {
        _doneSound.Play();
        
        if (_actuallyInteraction < 0)
        {
            //Done - Save Data
            dataCollector.GenerateJSON(_inputName);
            doneDisplay.SetActive(true);
        }
        
        //Disable previous interaction
        if (_actuallyInteraction < 5)
        {
            interactions[_actuallyInteraction + 1].SetActive(false);
            tvInteractionsDisplay[_actuallyInteraction + 1].SetActive(false);
        }

        if (_actuallyInteraction >= 0)
        {
            //Enable Interaction
            interactions[_actuallyInteraction].SetActive(true);
            tvInteractionsDisplay[_actuallyInteraction].SetActive(true);
        }
        
        _actuallyInteraction--;
    }

    public void AddInteractionData(string name)
    {
        InteractionData id = new InteractionData(name);
        id.SetInputName(dataCollector.dataName);
        id.SetSeconds(interactionTimer.GetTimeInSeconds());
        id.SetCountGrabInteraction(_countGrab);
        id.SetCountReleaseInteraction(_countRelease);
        id.SetCountTouchInteraction(_countTouch);
        dataCollector.AddData(id);
        ResetGrabReleaseTouchCounter();
    }

    private void ResetGrabReleaseTouchCounter()
    {
        print("StartGrabObject: " + _countGrab);
        print("ReleaseObject: " + _countRelease);
        print("TouchObject: " + _countTouch);
        _countGrab = 0;
        _countRelease = 0;
        _countTouch = 0;
    }
    
    public void StartGrabObject()
    {
        _countGrab++;
    }

    public void ReleaseObject()
    {
        _countRelease++;
    }

    public void TouchObject()
    {
        _countTouch++;
    }
}
