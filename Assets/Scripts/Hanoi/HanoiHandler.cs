using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiHandler : MonoBehaviour
{
    [Header("Hanoi Objects")]
    public List<Hanoi> hanoiObjects;
    
    [Header("Placed Correctly Objects")]
    public List<Hanoi> placedRightHanoiObjects;
    private int _hanoiObejectDoneCounter;

    [Header("Interaction Handler")]
    public InteractionHandler interactionHandler;
    
    [Header("Interaction Timer")]
    public InteractionTimer timer;

    private Coroutine _hanoiDone;

    private int _placedRight;
    private bool _allHanoiPlaced;

    public void StartHanoi()
    {
        placedRightHanoiObjects = new List<Hanoi>();
        
        if (!timer.TimerStarted())
            timer.StartTimer();
    }

    private void Update()
    {
        HanoiPlacedRight();
    }

    //Check if the objects are placed correctly
    public void HanoiPlacedRight()
    {
        foreach (Hanoi hanoi in hanoiObjects)
        {
            Debug.Log("CheckPlaced: " + hanoi.isPlacedRight + " : " + hanoi.gameObject.name);
            if (hanoi.isPlacedRight)
            {
                if(!placedRightHanoiObjects.Contains(hanoi))
                    placedRightHanoiObjects.Add(hanoi);
                
                Debug.Log("Korrekt platziert: " + placedRightHanoiObjects.Count);
            }
            else
            {
                if (placedRightHanoiObjects.Contains(hanoi))
                    placedRightHanoiObjects.Remove(hanoi);
                
                Debug.Log("Falsch platziert: " + placedRightHanoiObjects.Count);
            }
        }
        
        if (placedRightHanoiObjects.Count >= 4 && !_allHanoiPlaced)
        {
            _allHanoiPlaced = true;
            _hanoiDone = StartCoroutine(HanoiDone());
        }
        
        if(placedRightHanoiObjects.Count < 4 && _allHanoiPlaced)
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
                if(!placedRightHanoiObjects.Contains(hanoi))
                    placedRightHanoiObjects.Add(hanoi);
                
                Debug.Log("Korrekt platziert: " + placedRightHanoiObjects.Count);
            }
            else
            {
                if (placedRightHanoiObjects.Contains(hanoi))
                    placedRightHanoiObjects.Remove(hanoi);
                
                Debug.Log("Falsch platziert: " + placedRightHanoiObjects.Count);
            }
        }
        
        if(_hanoiDone != null)
            StopCoroutine(_hanoiDone);
    }

    private IEnumerator HanoiDone()
    {
        yield return new WaitForSeconds(2f);
        timer.StopTimer();
        timer.SetTimeInSeconds(timer.GetTimeInSeconds() - 2f);
        interactionHandler.AddInteractionData("Hanoi");
        interactionHandler.NextInteraction();
    }
}
