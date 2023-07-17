using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    [Header("Interaction Handler")]
    public InteractionHandler interactionHandler;

    private InteractionTimer _timer;

    private static Rigidbody _rb;
    private int _rollCounter;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _timer = interactionHandler.GetComponent<InteractionTimer>();
    }

    public void StartDiceInteraction()
    {
        _timer.StartTimer();
    }

    public void OnDice()
    {
        _rollCounter++;
        
        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);

        _rb.AddForce(transform.up * 100);
        _rb.AddTorque(dirX, dirY, dirZ);

        if (_rollCounter >= 5)
        {
            _timer.StopTimer();
            StartCoroutine(DiceIsFinished());
        }
    }

    private IEnumerator DiceIsFinished()
    {
        yield return new WaitForSeconds(2f);
        interactionHandler.EnableNextInteraction();
    }
}
