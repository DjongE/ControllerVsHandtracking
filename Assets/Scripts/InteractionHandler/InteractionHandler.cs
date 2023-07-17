using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [Header("Interactions Controller")]
    public List<GameObject> interactions;
    [Header("Television Interaction Objects")]
    public List<GameObject> tvInteractionsDisplay;

    public InteractionTimer interactionTimer;
    private int _countGrab;
    private int _countRelease;
    private int _countTouch;

    public DataCollector dataCollector;

    private int _actuallyInteraction;
    private AudioSource _doneSound;

    private void Start()
    {
        _doneSound = GetComponent<AudioSource>();
    }

    public void EnableNextInteraction()
    {
        _doneSound.Play();
        
        //Save Interaction Data
        AddInteractionData(interactions[0].name);
        
        //Disable previous interaction
        if (_actuallyInteraction > 0)
        {
            interactions[_actuallyInteraction - 1].SetActive(false);
            tvInteractionsDisplay[_actuallyInteraction - 1].SetActive(false);
        }

        //Enable Interaction
        interactions[_actuallyInteraction].SetActive(true);
        tvInteractionsDisplay[_actuallyInteraction].SetActive(true);

        _actuallyInteraction++;
    }

    public void ResetActuallyInteractionCounter()
    {
        _actuallyInteraction = 0;
    }

    private void AddInteractionData(string name)
    {
        InteractionData id = new InteractionData(name);
        id.SetInputName(dataCollector.dataName);
        id.SetSeconds(interactionTimer.GetTimeInSeconds());
        id.SetCountGrabInteraction(_countGrab);
        id.SetCountReleaseInteraction(_countRelease);
        id.SetCountTouchInteraction(_countTouch);
        dataCollector.AddData(id);
    }
    
    public void StartGrabDice()
    {
        _countGrab++;
    }

    public void ReleaseDice()
    {
        _countRelease++;
    }

    public void TouchDice()
    {
        _countTouch++;
    }
}
