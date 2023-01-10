using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class ControllerHandtrackingHandler : MonoBehaviour
{
    public InteractionModeManager.ManagedInteractorStatus imm;

    private void Awake()
    {
        
    }

    private void Update()
    {
        Debug.Log(imm.CurrentMode);
    }
}
