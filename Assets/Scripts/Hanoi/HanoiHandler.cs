using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiHandler : MonoBehaviour
{
    public List<Hanoi> hanoiObjects;
    public List<Hanoi> _placedRightHanoiObjects;
    private int _hanoiObejectDoneCounter;

    public InteractionHandler interactionHandler;
    public InteractionTimer timer;

    private Coroutine _hanoiDone;
    
    public AudioSource doneSound;

    private int _placedRight;
    private bool _allHanoiPlaced;

    public void StartHanoi()
    {
        _placedRightHanoiObjects = new List<Hanoi>();
        
        if (!timer.TimerStarted())
            timer.StartTimer();
    }

    private void Update()
    {
        HanoiPlacedRight();
    }

    public void HanoiPlacedRight()
    {
        //doneSound.Play();
        Debug.Log("Hanoi Counter Placed: " + _placedRight);

        foreach (Hanoi hanoi in hanoiObjects)
        {
            Debug.Log("CheckPlaced: " + hanoi.isPlacedRight + " : " + hanoi.gameObject.name);
            if (hanoi.isPlacedRight)
            {
                if(!_placedRightHanoiObjects.Contains(hanoi))
                    _placedRightHanoiObjects.Add(hanoi);
                
                Debug.Log("Korrekt platziert: " + _placedRightHanoiObjects.Count);
            }
            else
            {
                if (_placedRightHanoiObjects.Contains(hanoi))
                    _placedRightHanoiObjects.Remove(hanoi);
                
                Debug.Log("Falsch platziert: " + _placedRightHanoiObjects.Count);
            }
        }
        
        if (_placedRightHanoiObjects.Count >= 4 && !_allHanoiPlaced)
        {
            _allHanoiPlaced = true;
            _hanoiDone = StartCoroutine(HanoiDone());
        }
        
        if(_placedRightHanoiObjects.Count < 4 && _allHanoiPlaced)
        {
            _allHanoiPlaced = false;
            if(_hanoiDone != null)
                StopCoroutine(_hanoiDone);
        }
    }

    public void HanoiRemoved()
    {
        _allHanoiPlaced = false;
        
        foreach (Hanoi hanoi in hanoiObjects)
        {
            Debug.Log("CheckPlaced: " + hanoi.isPlacedRight + " : " + hanoi.gameObject.name);
            if (hanoi.isPlacedRight)
            {
                if(!_placedRightHanoiObjects.Contains(hanoi))
                    _placedRightHanoiObjects.Add(hanoi);
                
                Debug.Log("Korrekt platziert: " + _placedRightHanoiObjects.Count);
            }
            else
            {
                if (_placedRightHanoiObjects.Contains(hanoi))
                    _placedRightHanoiObjects.Remove(hanoi);
                
                Debug.Log("Falsch platziert: " + _placedRightHanoiObjects.Count);
            }
        }
        
        if(_hanoiDone != null)
            StopCoroutine(_hanoiDone);
        
        Debug.Log("Hanoi Counter Removed: " + _placedRight);
    }

    private IEnumerator HanoiDone()
    {
        Debug.Log("STOP ICH BIMS JENS UND STEFAN");
        yield return new WaitForSeconds(2f);
        timer.StopTimer();
        timer.SetTimeInSeconds(timer.GetTimeInSeconds() - 2f);
        interactionHandler.AddInteractionData("Hanoi");
        interactionHandler.NextInteraction();
    }
}
