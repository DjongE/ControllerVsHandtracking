using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Car Sound")]
    public AudioSource carSound;

    private bool _pitchSound;
    private float _time;
    private float _duration = 0.8f;

    private void Start()
    {
        //StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(5f);
        PitchHigher();
        yield return new WaitForSeconds(5f);
        PitchLower();
    }

    private void Update()
    {
        if (_pitchSound)
        {
            //Higher
            carSound.pitch = Mathf.Lerp(1f, 1.3f, _time / _duration);
            _time += Time.deltaTime;
        }
        else
        {
            //Lower
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
