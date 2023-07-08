using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [Header("Interactions")]
    public List<GameObject> interactions;

    [Header("Television Interaction Objects")]
    public List<GameObject> tvInteractions;

    private int _actuallyInteraction;
    private AudioSource _doneSound;

    private void Start()
    {
        _doneSound = GetComponent<AudioSource>();
    }

    public void EnableNextInteraction()
    {
        _doneSound.Play();
        
        //Disable previous interaction
        if (_actuallyInteraction > 0)
        {
            interactions[_actuallyInteraction - 1].SetActive(false);
            tvInteractions[_actuallyInteraction - 1].SetActive(false);
        }

        //Enable Interaction
        interactions[_actuallyInteraction].SetActive(true);
        tvInteractions[_actuallyInteraction].SetActive(true);

        _actuallyInteraction++;
    }
}
