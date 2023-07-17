using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiHandler : MonoBehaviour
{
    public InteractionHandler interactionHandler;
    
    public AudioSource doneSound;

    private int _placedRight;
    
    public void HanoiPlacedRight()
    {
        doneSound.Play();
        _placedRight++;

        if (_placedRight >= 5)
        {
            StartCoroutine(HanoiDone());
        }
    }

    public void HanoiRemoved()
    {
        _placedRight--;
    }

    private IEnumerator HanoiDone()
    {
        yield return new WaitForSeconds(2f);
        interactionHandler.EnableNextInteraction();
    }
}
