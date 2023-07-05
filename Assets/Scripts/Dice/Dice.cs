using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    private static Rigidbody _rb;
    public static Vector3 diceVelocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnDice()
    {
        diceVelocity = _rb.velocity;

        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);

        _rb.AddForce(transform.up * 100);
        _rb.AddTorque(dirX, dirY, dirZ);
    }
}
