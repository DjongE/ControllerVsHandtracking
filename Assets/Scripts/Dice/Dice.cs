using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    [Header("Interaction Handler")]
    public InteractionHandler interactionHandler;

    public InteractionTimer timer;

    private static Rigidbody _rb;
    private int _rollCounter;
    private Coroutine _diceFinished;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void StartDiceInteraction()
    {
        timer.StartTimer();
    }

    public void OnDice()
    {
        _rollCounter++;
        
        Debug.Log("Roll Counter: " + _rollCounter);
        //float dirX = Random.Range(0, 500);
        //float dirY = Random.Range(0, 500);
        //float dirZ = Random.Range(0, 500);

        //_rb.AddForce(transform.up * 100);
        //_rb.AddTorque(dirX, dirY, dirZ);

        if (_rollCounter >= 5)
        {
            Debug.Log("Roll Counter Done");
            timer.StopTimer();
            if(_diceFinished == null)
                _diceFinished = StartCoroutine(DiceIsFinished());
        }
    }

    private IEnumerator DiceIsFinished()
    {
        yield return new WaitForSeconds(2f);
        interactionHandler.EnableNextInteraction();
    }
}
