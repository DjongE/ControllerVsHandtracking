using System.Collections;
using UnityEngine;

public class VirtualButtons : MonoBehaviour
{
    [Header("Interaction Handler")]
    public InteractionHandler interactionHandler;
    
    [Header("Interaction Timer")]
    public InteractionTimer timer;
    
    [Header("Car")]
    public Transform car;
    
    [Header("Movement datas")]
    public float move;
    public float moveSpeed;
    public float rotation;
    public float rotateSpeed;

    [Header("Input")]
    public bool clickedLeft;
    public bool clickedRight;
    public bool clickedUp;
    public bool clickedDown;

    private Coroutine _done;

    private void LateUpdate()
    {
        if(clickedLeft || clickedRight || clickedUp || clickedDown)
            Drive();
    }

    public void VirtualButtonsIsDone()
    {
        if(_done == null)
            _done = StartCoroutine(Done());
    }

    private IEnumerator Done()
    {
        timer.StopTimer();
        interactionHandler.AddInteractionData("VirtualButtons");
        yield return new WaitForSeconds(2f);
        interactionHandler.NextInteraction();
    }
    
    private void Drive()//Lets drive the car
    {
        car.Translate(0f, 0f, move);
        car.Rotate(0f, rotation, 0f);
        
        if(!timer.TimerStarted())
            timer.StartTimer();
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
