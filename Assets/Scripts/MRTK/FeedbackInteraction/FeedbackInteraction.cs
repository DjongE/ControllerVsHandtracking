using System;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;

public class FeedbackInteraction : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (gameObject.TryGetComponent(out MRTKBaseInteractable baseInteractable))
        {
            
            //LookAt (IsGazeHovered)
            baseInteractable.IsGazeHovered.OnEntered.AddListener(OnIsLookAtHovered);
            baseInteractable.IsGazeHovered.OnExited.AddListener(OnIsLookAtUnhovered);

        
            //Hand Ray
            baseInteractable.IsRayHovered.OnEntered.AddListener(OnIsRayHovered);
            baseInteractable.IsRayHovered.OnExited.AddListener(OnIsRayUnhovered);

            //Touched
            baseInteractable.IsGrabHovered.OnEntered.AddListener(OnIsTochedSelected);
            baseInteractable.IsGrabHovered.OnExited.AddListener(OnIsTochedUnselected);
        
            //Grabbed
            baseInteractable.IsGrabSelected.OnEntered.AddListener(OnIsGrabSelected);
            baseInteractable.IsGrabSelected.OnExited.AddListener(OnIsGrabUnselected);
        }
        else
        {
            Debug.Log(transform.name + ": StatefulInteracable not found!");
        }
    }

    #region LookAt
    protected virtual void OnIsLookAtHovered(float args)
    {
        Debug.Log("OnIsLookAtHovered: " + transform.name);
    }
    
    protected virtual void OnIsLookAtUnhovered(float args)
    {
        Debug.Log("OnIsLookAtUnhovered: " + transform.name);
    }
    #endregion

    #region RayHovered
    protected virtual void OnIsRayHovered(float args)
    {
        Debug.Log("OnIsRayHovered: " + transform.name);
    }

    protected virtual void OnIsRayUnhovered(float args)
    {
        Debug.Log("OnIsRayUnhovered: " + transform.name);
    }
    #endregion

    #region Touched
    protected virtual void OnIsTochedSelected(float args)
    {
        Debug.Log("OnIsTochedSelected: " + transform.name);
    }
    
    protected virtual void OnIsTochedUnselected(float args)
    {
        Debug.Log("OnIsTochedUnselected: " + transform.name);
    }
    #endregion

    #region Grabbed
    protected virtual void OnIsGrabSelected(float args)
    {
        Debug.Log("OnIsGrabSelected: " + transform.name);
    }

    protected virtual void OnIsGrabUnselected(float args)
    {
        Debug.Log("OnIsGrabUnselected: " + transform.name);
    }
    #endregion
}
