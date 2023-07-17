using UnityEngine;

public class InteractionTimer : MonoBehaviour
{
    private bool _timerStopped;
    private float _time;
    
    private void Update()
    {
        if (!_timerStopped)
        {
            _time += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        _time = 0;
        _timerStopped = false;
    }

    public void StopTimer()
    {
        _timerStopped = true;
    }

    public float GetTimeInSeconds()
    {
        return _time;
    }
}
