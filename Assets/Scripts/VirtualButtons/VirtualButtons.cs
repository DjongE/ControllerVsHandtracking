using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualButtons : MonoBehaviour
{
    [Header("Car")]
    public Transform car;
    
    public float move;
    public float moveSpeed;
    public float rotation;
    public float rotateSpeed;

    [Header("Input")]
    public bool clickedLeft;
    public bool clickedRight;
    public bool clickedUp;
    public bool clickedDown;

    private void LateUpdate()
    {
        if(clickedLeft || clickedRight || clickedUp || clickedDown)
            Drive();
    }

    private void Drive()
    {
        car.Translate(0f, 0f, move);
        car.Rotate(0f, rotation, 0f);
    }

    public void PressLeftButton()
    {
        //Rotate Car Left
        clickedLeft = true;
        rotation = 1 * -rotateSpeed * Time.fixedDeltaTime;
    }
    
    public void ReleaseLeftButton()
    {
        //Rotate Car Left
        clickedLeft = false;
        rotation = 0;
    }

    public void PressRightButton()
    {
        //Rotate Car Right
        clickedRight = true;
        rotation = 1 * rotateSpeed * Time.fixedDeltaTime;
    }
    public void ReleaseRightButton()
    {
        //Rotate Car Right
        clickedRight = false;
        rotation = 0;
    }

    public void PressUpButton()
    {
        //Drive Forward
        clickedUp = true;
        move = 1 * moveSpeed * Time.fixedDeltaTime;
    }
    public void ReleaseUpButton()
    {
        //Drive Forward
        clickedUp = false;
        move = 0;
    }

    public void PressDownButton()
    {
        //Drive Backwards
        clickedDown = true;
        move = 1 * -moveSpeed * Time.fixedDeltaTime;
    }
    public void ReleaseDownButton()
    {
        //Drive Backwards
        clickedDown = false;
        move = 0;
    }
}
