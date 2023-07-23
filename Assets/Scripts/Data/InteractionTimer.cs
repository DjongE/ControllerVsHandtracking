using System;
using UnityEngine;

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

    public void StartTimer()
    {
        _time = 0;
        _timerStarted = true;
        Debug.Log("Timer gestartet: " + _time);
    }

    public void StopTimer()
    {
        _timerStarted = false;
        Debug.Log("Timer gestopt: " + _time);
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
