using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Car Sound")]
    public AudioSource carSound;

    private bool _pitchSound;
    private float _time;
    private float _duration = 0.8f;

    private void Update()
    {
        if (_pitchSound)
        {
            //Pitch the sound higher
            carSound.pitch = Mathf.Lerp(1f, 1.3f, _time / _duration);
            _time += Time.deltaTime;
        }
        else
        {
            //Pitch the sound lower
            carSound.pitch = Mathf.Lerp(1.3f, 1f, _time / _duration);
            _time += Time.deltaTime;
        }
    }

    public void PitchHigher()
    {
        _pitchSound = true;
        _time = 0;
    }

    public void PitchLower()
    {
        _pitchSound = false;
        _time = 0;
    }
}
