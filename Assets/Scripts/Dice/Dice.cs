using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    [Header("Interaction Handler")]
    public InteractionHandler interactionHandler;

    [Header("Interaction Timer")]
    public InteractionTimer timer;

    private Rigidbody _rb;
    private int _rollCounter;
    private Coroutine _diceFinished;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void StartDiceInteraction()
    {
        if(!timer.TimerStarted())
            timer.StartTimer();
    }

    //Executed when the cube is released
    public void OnDice()
    {
        _rollCounter++;
        
        _rb.AddTorque(Random.Range(0,500),Random.Range(0,500),Random.Range(0,500));

        if (_rollCounter > 4)
        {
            if(_diceFinished == null)
                _diceFinished = StartCoroutine(DiceIsFinished());
        }
    }

    //Is executed when the user has rolled the dice at least 5 times
    private IEnumerator DiceIsFinished()
    {
        timer.StopTimer();
        interactionHandler.AddInteractionData("Dice");
        yield return new WaitForSeconds(2f);
        interactionHandler.NextInteraction();
    }
}
