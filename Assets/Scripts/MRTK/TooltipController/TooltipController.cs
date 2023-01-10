using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.MixedReality.Toolkit;
using Unity.Collections;
using UnityEngine;

public class TooltipController : FeedbackInteraction
{
    [Header("Tooltip")]
    public GameObject tooltip;

    [Header("Disabled at start")]
    public bool disableOnStart;
    
    [Header("Tooltip always activated")]
    public bool activateAlways;

    [Header("Activate Tooltip")]
    [Tooltip("When should the outline be activated?")]
    public bool activateOnLookAt;
    public bool activateOnHandRay;
    public bool activateOnTouch;
    public bool activateOnGrab;
    
    private bool _isOnLookAt;
    private bool _isOnHandRay;
    private bool _isOnTouch;
    private bool _isOnGrab;

    private SnapDropZone _snapDropZone;

    [Header("Activate on SnapDropZone")]
    public bool activateOnSnapDropObjectLookAt;
    public bool activateOnSnapDropObjectHandRay;
    public bool activateOnSnapDropObjectTouch;
    public bool activateOnSnapDropObjectGrab;

    private void Start()
    {
        Init();
        InitSnapDropZoneTooltip();
    }

    protected override void Init()
    {
        base.Init();
        
        if (activateAlways)
        {
            tooltip.SetActive(true);
            disableOnStart = false;
        }
        
        if(disableOnStart)
            tooltip.SetActive(false);
    }

    private void InitSnapDropZoneTooltip()
    {
        if (gameObject.TryGetComponent(out SnapDropZone snap))
        {
            _snapDropZone = snap;

            if (_snapDropZone.snapDropObject.TryGetComponent(out MRTKBaseInteractable baseInteractable))
            {
                //LookAt (IsGazeHovered)
                baseInteractable.IsGazeHovered.OnEntered.AddListener(SnapDropZoneOnIsLookAtHovered);
                baseInteractable.IsGazeHovered.OnExited.AddListener(SnapDropZoneOnIsLookAtUnhovered);

        
                //Hand Ray
                baseInteractable.IsRayHovered.OnEntered.AddListener(SnapDropZoneOnIsRayHovered);
                baseInteractable.IsRayHovered.OnExited.AddListener(SnapDropZoneOnIsRayUnhovered);

                //Touched
                baseInteractable.IsGrabHovered.OnEntered.AddListener(SnapDropZoneOnIsTochedSelected);
                baseInteractable.IsGrabHovered.OnExited.AddListener(SnapDropZoneOnIsTochedUnselected);
        
                //Grabbed
                baseInteractable.IsGrabSelected.OnEntered.AddListener(SnapDropZoneOnIsGrabSelected);
                baseInteractable.IsGrabSelected.OnExited.AddListener(SnapDropZoneOnIsGrabUnselected);
            }
        }
    }

    #region Normal Object
    protected override void OnIsLookAtHovered(float args)
    {
        //base.OnIsLookAtHovered(args);

        if (activateOnLookAt)
        {
            _isOnLookAt = true;
            ToggleTooltip();
        }
    }

    protected override void OnIsLookAtUnhovered(float args)
    {
        //base.OnIsLookAtUnhovered(args);

        if (!activateAlways && activateOnLookAt)
        {
            _isOnLookAt = false;
            ToggleTooltip();
        }
    }

    protected override void OnIsRayHovered(float args)
    {
        //base.OnIsRayHovered(args);
        
        if (activateOnHandRay)
        {
            _isOnHandRay = true;
            ToggleTooltip();
        }
    }

    protected override void OnIsRayUnhovered(float args)
    {
        //base.OnIsRayUnhovered(args);

        if (!activateAlways && activateOnHandRay)
        {
            _isOnHandRay = false;
            ToggleTooltip();
        }
    }

    protected override void OnIsTochedSelected(float args)
    {
        //base.OnIsTochedSelected(args);
        
        if (activateOnTouch)
        {
            _isOnTouch = true;
            ToggleTooltip();
        }
    }

    protected override void OnIsTochedUnselected(float args)
    {
        //base.OnIsTochedUnselected(args);

        if (!activateAlways && activateOnTouch)
        {
            _isOnTouch = false;
            ToggleTooltip();
        }
    }

    protected override void OnIsGrabSelected(float args)
    {
        //base.OnIsGrabSelected(args);
        
        if (activateOnGrab)
        {
            _isOnGrab = true;
            ToggleTooltip();
        }
    }

    protected override void OnIsGrabUnselected(float args)
    {
        //base.OnIsGrabUnselected(args);

        if (!activateAlways && activateOnGrab)
        {
            _isOnGrab = false;
            ToggleTooltip();
        }
    }
    #endregion
    
    #region SnapDropZone Object

    private void SnapDropZoneOnIsLookAtHovered(float args)
    {
        if (activateOnSnapDropObjectLookAt)
        {
            _isOnLookAt = true;
            ToggleTooltip();
        }
    }
    private void SnapDropZoneOnIsLookAtUnhovered(float args)
    {
        if (!activateAlways && activateOnSnapDropObjectLookAt)
        {
            _isOnLookAt = false;
            ToggleTooltip();
        }
    }
    
    private void SnapDropZoneOnIsRayHovered(float args)
    {
        if (activateOnSnapDropObjectHandRay)
        {
            _isOnLookAt = true;
            ToggleTooltip();
        }
    }
    private void SnapDropZoneOnIsRayUnhovered(float args)
    {
        if (!activateAlways && activateOnSnapDropObjectHandRay)
        {
            _isOnLookAt = false;
            ToggleTooltip();
        }
    }
    
    private void SnapDropZoneOnIsTochedSelected(float args)
    {
        if (activateOnSnapDropObjectTouch)
        {
            _isOnLookAt = true;
            ToggleTooltip();
        }
    }
    private void SnapDropZoneOnIsTochedUnselected(float args)
    {
        if (!activateAlways && activateOnSnapDropObjectTouch)
        {
            _isOnLookAt = false;
            ToggleTooltip();
        }
    }
    
    private void SnapDropZoneOnIsGrabSelected(float args)
    {
        if (activateOnSnapDropObjectGrab)
        {
            _isOnLookAt = true;
            ToggleTooltip();
        }
    }
    private void SnapDropZoneOnIsGrabUnselected(float args)
    {
        if (!activateAlways && activateOnSnapDropObjectGrab)
        {
            _isOnLookAt = false;
            ToggleTooltip();
        }
    }
    #endregion

    private void ToggleTooltip()
    {
        //Debug.Log(_isOnLookAt + " " + _isOnHandRay + " " + _isOnTouch + " " + _isOnGrab);
        if (_isOnLookAt || _isOnHandRay || _isOnTouch || _isOnGrab)
        {
            tooltip.SetActive(true);
        }
        else
        {
            tooltip.SetActive(false);
        }
    }
}
