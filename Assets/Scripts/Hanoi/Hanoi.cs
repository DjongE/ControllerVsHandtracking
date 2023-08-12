using UnityEngine;

public class Hanoi : MonoBehaviour
{
    [Header("Information Sound")]
    public AudioSource placedRight;
    
    [Header("Define the lower and upper object of this object")]
    public GameObject underHanoi;
    public GameObject aboveHanoi;

    private RaycastHit _hit;
    
    [Header("Hanoi Object Is Placed Right")]
    public bool isPlacedRight;
    
    private bool _underObjectIsRight1;
    private bool _underObjectIsRight2;
    private bool _aboveObjectIsRight1;
    private bool _aboveObjectIsRight2;

    private void FixedUpdate()
    {
        UnderHanoiPlacedRight();//Checks if the lower object is correct
        AboveHanoiPlacedRight();//Checks if the upper object is correct

        if ((_underObjectIsRight1 || _underObjectIsRight2 ) && (_aboveObjectIsRight1 || _aboveObjectIsRight2) && !isPlacedRight)
        {
            isPlacedRight = true;
            placedRight.Play();
        }

        if ((!_underObjectIsRight1 && !_underObjectIsRight2) || (!_aboveObjectIsRight1 && !_aboveObjectIsRight2) &&
            isPlacedRight)
        {
            isPlacedRight = false;
        }
    }

    //Check if the lower hanoi object is correctly
    private void UnderHanoiPlacedRight()
    {
        if (underHanoi == null)
        {
            //Unterstes Objekt (Gelb)
            _underObjectIsRight1 = true;
        }
        
        if (underHanoi != null)
        {
            if (!_underObjectIsRight2)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) * 0.01f, out _hit, 0.05f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 0.05f, Color.yellow);
                    if (_hit.transform.gameObject.name.Equals(underHanoi.name))
                    {
                        Debug.Log("Under 1: true " + transform.name);
                        _underObjectIsRight1 = true;
                    }
                    else
                    {
                        Debug.Log("Under 1: false " + transform.name);
                        _underObjectIsRight1 = false;
                    }
                }
            }

            if (!_underObjectIsRight1)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up) * 0.01f, out _hit, 0.05f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 0.05f, Color.yellow);
                    if (_hit.transform.gameObject.name.Equals(underHanoi.name))
                    {
                        Debug.Log("Under 2: true " + transform.name);
                        _underObjectIsRight2 = true;
                    }
                    else
                    {
                        Debug.Log("Under 2: false " + transform.name);
                        _underObjectIsRight2 = false;
                    }
                }
            }
        }
    }

    //Check if the upper hanoi object is correctly
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
