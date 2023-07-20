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

    public void StartHanoi()
    {
        if (!timer.TimerStarted())
            timer.StartTimer();
    }
    
    public void HanoiPlacedRight()
    {
        doneSound.Play();
        _placedRight++;

        if (_placedRight >= 3)
        {
            if(_hanoiDone == null)
                _hanoiDone = StartCoroutine(HanoiDone());
        }
    }

    public void HanoiRemoved()
    {
        _placedRight--;
    }

    private IEnumerator HanoiDone()
    {
        timer.StopTimer();
        interactionHandler.AddInteractionData("Hanoi");
        yield return new WaitForSeconds(2f);
        interactionHandler.NextInteraction();
    }
}
