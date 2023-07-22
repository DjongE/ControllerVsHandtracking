using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiHandler : MonoBehaviour
{
    public InteractionHandler interactionHandler;
    public InteractionTimer timer;

    private Coroutine _hanoiDone;
    
    public AudioSource doneSound;

    private int _placedRight;
    private bool _allHanoiPlaced;

    public void StartHanoi()
    {
        if (!timer.TimerStarted())
            timer.StartTimer();
    }
    
    public void HanoiPlacedRight()
    {
        doneSound.Play();
        _placedRight++;
        Debug.Log("Hanoi Counter Placed: " + _placedRight);

        if (_placedRight > 0 && !_allHanoiPlaced)
        {
            _allHanoiPlaced = true;
            _hanoiDone = StartCoroutine(HanoiDone());
        }
    }

    public void HanoiRemoved()
    {
        if (_placedRight > 0)
        {
            _placedRight--;
        }
        
        _allHanoiPlaced = false;
        
        if(_hanoiDone != null)
            StopCoroutine(_hanoiDone);
        
        Debug.Log("Hanoi Counter Removed: " + _placedRight);
    }

    private IEnumerator HanoiDone()
    {
        timer.StopTimer();
        interactionHandler.AddInteractionData("Hanoi");
        yield return new WaitForSeconds(2f);
        interactionHandler.NextInteraction();
    }
}
