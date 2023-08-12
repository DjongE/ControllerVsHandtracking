using UnityEngine;

/**
 * A normal timer to be able to record the execution speeds of the interactions
 */
public class InteractionTimer : MonoBehaviour
{
    private bool _timerStarted;
    private float _time;

    private void Update()
    {
        if (_timerStarted)
        {
            _time += Time.deltaTime;
        }
    }

    //Start the timer
    public void StartTimer()
    {
        _time = 0;
        _timerStarted = true;
    }

    //Stop the timer
    public void StopTimer()
    {
        _timerStarted = false;
    }

    public bool TimerStarted()
    {
        return _timerStarted;
    }

    public float GetTimeInSeconds()
    {
        return _time;
    }

    public void SetTimeInSeconds(float newTime)
    {
        _time = newTime;
    }
}
