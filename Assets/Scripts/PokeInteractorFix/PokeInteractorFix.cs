using System;
using UnityEngine;

public class PokeInteractorFix : MonoBehaviour
{
    public void FixPoke()
    {
        transform.localPosition = new Vector3(0.005f, -0.01788f, 0.0678f);
    }

    private void OnEnable()
    {
        transform.localPosition = new Vector3(0.005f, -0.01788f, 0.0678f);
    }

    private void Start()
    {
        transform.localPosition = new Vector3(0.005f, -0.01788f, 0.0678f);
    }

    private void Awake()
    {
        transform.localPosition = new Vector3(0.005f, -0.01788f, 0.0678f);
    }

    private void Update()
    {
        transform.localPosition = new Vector3(0.005f, -0.01788f, 0.0678f);
    }
}
