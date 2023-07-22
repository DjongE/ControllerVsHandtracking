using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    [Header("Interaction Handler")]
    public InteractionHandler interactionHandler;

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

    public void OnDice()
    {
        _rollCounter++;
        
        Debug.Log("Roll Counter: " + _rollCounter);
        _rb.AddTorque(Random.Range(0,500),Random.Range(0,500),Random.Range(0,500));

        if (_rollCounter > 4)
        {
            Debug.Log("Roll Counter Done");
            if(_diceFinished == null)
                _diceFinished = StartCoroutine(DiceIsFinished());
        }
    }

    private IEnumerator DiceIsFinished()
    {
        timer.StopTimer();
        interactionHandler.AddInteractionData("Dice");
        yield return new WaitForSeconds(2f);
        interactionHandler.NextInteraction();
    }
}
