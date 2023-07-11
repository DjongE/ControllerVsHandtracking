using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiHandler : MonoBehaviour
{
    public AudioSource doneSound;
    
    public void HanoiPlacedRight()
    {
        doneSound.Play();
    }
}
