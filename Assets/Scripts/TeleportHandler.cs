using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Used to implement custom teleportation functions.
/// </summary>
public class TeleportHandler : MonoBehaviour
{
    private TeleportationArea[] teleportationAreas;
    private HandTrackingObserver handTrackingObserver;

    private void Awake()
    {
        teleportationAreas = FindObjectsOfType<TeleportationArea>();
        handTrackingObserver = FindObjectOfType<HandTrackingObserver>();
    }

    // Start is called before the first frame update
    void Start()
    {
        handTrackingObserver.articulatedHandShown.AddListener(AdjustTeleportTrigger);
        handTrackingObserver.articulatedHandHidden.AddListener(AdjustTeleportTrigger);

        AdjustTeleportTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Teleport on deactivate with controller and on select enter with handtracking
    private void AdjustTeleportTrigger()
    {
        if (HandTrackingObserver.IsHandTrackingActive())
        {
            // hand tracking is active
            for (int i = 0; i < teleportationAreas.Length; i++)
            {
                teleportationAreas[i].teleportTrigger = BaseTeleportationInteractable.TeleportTrigger.OnSelectEntered;
            }
        }
        else
        {
            // controller is active
            for (int i = 0; i < teleportationAreas.Length; i++)
            {
                teleportationAreas[i].teleportTrigger = BaseTeleportationInteractable.TeleportTrigger.OnActivated;
            }
        }
    }
}
