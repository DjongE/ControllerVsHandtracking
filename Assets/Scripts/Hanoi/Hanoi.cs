using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Hanoi : MonoBehaviour
{
    public HanoiHandler hanoiHandler;
    public GameObject underHanoi;
    public GameObject aboveHanoi;

    private RaycastHit _hit;
    private float _damp = 0.01f;
    private bool _isPlacedRight;
    private bool _underObjectisRight1;
    private bool _underObjectisRight2;
    private bool _aboveObjectIsRight1;
    private bool _aboveObjectIsRight2;

    private void FixedUpdate()
    {
        UnderHanoiPlacedRight();
        AboveHanoiPlacedRight();

        if ((_underObjectisRight1 || _underObjectisRight2 ) && (_aboveObjectIsRight1 || _aboveObjectIsRight2) && !_isPlacedRight)
        {
            Debug.Log("Hanoi: Ja " + transform.name);
            hanoiHandler.HanoiPlacedRight();
            _isPlacedRight = true;
        }

        if ((!_underObjectisRight1 && !_underObjectisRight2) || (!_aboveObjectIsRight1 && !_aboveObjectIsRight2) &&
            _isPlacedRight)
        {
            Debug.Log("Hanoi: Nein " + transform.name);
            hanoiHandler.HanoiRemoved();
            _isPlacedRight = false;
        }
    }

    private void UnderHanoiPlacedRight()
    {
        if (underHanoi == null)
        {
            //Unterstes Objekt (Gelb)
            _underObjectisRight1 = true;
        }
        
        if (underHanoi != null)
        {
            if (!_underObjectisRight2)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 0.01f, out _hit, 0.05f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 0.05f, Color.yellow);
                    if (_hit.transform.gameObject.name.Equals(underHanoi.name))
                    {
                        Debug.Log("Under 1: true " + transform.name);
                        _underObjectisRight1 = true;
                    }
                    else
                    {
                        Debug.Log("Under 1: false " + transform.name);
                        _underObjectisRight1 = false;
                    }
                }
            }

            if (!_underObjectisRight1)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up) * 0.01f, out _hit, 0.05f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 0.05f, Color.yellow);
                    if (_hit.transform.gameObject.name.Equals(underHanoi.name))
                    {
                        Debug.Log("Under 2: true " + transform.name);
                        _underObjectisRight2 = true;
                    }
                    else
                    {
                        Debug.Log("Under 2: false " + transform.name);
                        _underObjectisRight2 = false;
                    }
                }
            }
        }
    }

    private void AboveHanoiPlacedRight()
    {
        if (aboveHanoi == null)
        {
            //Gilt nur für ganz oben (Rot)
            _aboveObjectIsRight1 = true;
        }
        
        if(aboveHanoi != null)
        {
            if (!_aboveObjectIsRight2)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 0.01f, out _hit, 0.05f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 0.05f, Color.yellow);
                    if (_hit.transform.gameObject.name.Equals(aboveHanoi.name))
                    {
                        Debug.Log("Above 1: true " + transform.name);
                        _aboveObjectIsRight1 = true;
                    }
                    else
                    {
                        Debug.Log("Above 1: false " + transform.name);
                        _aboveObjectIsRight1 = false;
                    }
                }
            }

            if (!_aboveObjectIsRight1)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up) * 0.01f, out _hit, 0.05f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 0.05f, Color.yellow);
                    if (_hit.transform.gameObject.name.Equals(aboveHanoi.name))
                    {
                        Debug.Log("Above 2: true");
                        _aboveObjectIsRight2 = true;
                    }
                    else
                    {
                        Debug.Log("Above 2: false");
                        _aboveObjectIsRight2 = false;
                    }
                }
            }
        }
    }
}
