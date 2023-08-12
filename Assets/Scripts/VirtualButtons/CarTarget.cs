using UnityEngine;

public class CarTarget : MonoBehaviour
{
    [Header("Target")]
    public AudioSource arrivedTarget;
    
    [Header("Virtual Buttons")]
    public VirtualButtons virtualButtons;
    
    [Header("Car object")]
    public GameObject car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(car))
        {
            virtualButtons.VirtualButtonsIsDone();
            arrivedTarget.Play();
        }
    }
}
