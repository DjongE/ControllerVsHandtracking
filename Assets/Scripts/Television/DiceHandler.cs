using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHandler : MonoBehaviour
{
    public GameObject dice;
    
    public void EnableDiceInteraction()
    {
        dice.SetActive(true);
    }
}
