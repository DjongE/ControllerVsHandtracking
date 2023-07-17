using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject respawnObject;
    private GameObject _actuallyThrowObject;
    private Vector3 _respawnPoint;

    private void Start()
    {
        if(respawnPoint == null)
            respawnPoint = transform;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("RepawnDelay: " + other.gameObject.name);
        if (other.gameObject.name.Equals("ThrowCube(Clone)"))
        {
            StartCoroutine(RespawnDelay());
        }
    }
    
    private IEnumerator RespawnDelay()
    {
        Debug.Log("RepawnDelay");
        yield return new WaitForSeconds(3f);
        Respawn();
    }

    private void Respawn()
    {
        Destroy(_actuallyThrowObject);
        _actuallyThrowObject = Instantiate(respawnObject, respawnPoint.position, Quaternion.Euler(new Vector3(0,0,0)));
        _actuallyThrowObject.SetActive(true);
    }
}
