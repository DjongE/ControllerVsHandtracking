using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit;

public class RayActivator : MonoBehaviour
{
    [SerializeField]
    public InputActionReference actiavateRay;
    private InputAction _activateRay;

    public bool showRayPersistently = false;

    public UnityEvent<InputAction.CallbackContext> OnActivateRay;
    public UnityEvent<InputAction.CallbackContext> OnDeactivateRay;

    private ArticulatedHandController articulatedHandController;
    private bool isArticulatedHandModelSetup = false;

    private bool isRayActive = false;

    private GameObject rayInteractor;


    private void Awake()
    {
        rayInteractor = GetComponentInChildren<MRTKRayInteractor>().gameObject;
        rayInteractor.SetActive(false);
        _activateRay = actiavateRay.action;

        articulatedHandController = GetComponent<ArticulatedHandController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _activateRay.started += OnActivateRay.Invoke;
        _activateRay.canceled += OnDeactivateRay.Invoke;

        OnActivateRay.AddListener(delegate { ToggleRay(true); });
        OnDeactivateRay.AddListener(delegate { ToggleRay(false); });

    }

    private void OnDestroy()
    {
        _activateRay.started -= OnActivateRay.Invoke;
        _activateRay.canceled -= OnDeactivateRay.Invoke;

        OnActivateRay.RemoveListener(delegate { ToggleRay(true); });
        OnDeactivateRay.RemoveListener(delegate { ToggleRay(false); });
    }

    // Update is called once per frame
    void Update()
    {
        if (!isArticulatedHandModelSetup)
        {
            if (articulatedHandController.model)
            {
                articulatedHandController.model.gameObject.GetComponent<CustomHandGestures>().onGestureRayShow.AddListener(delegate { ToggleRay(true); });
                articulatedHandController.model.gameObject.GetComponent<CustomHandGestures>().onGestureRayHide.AddListener(delegate { ToggleRay(false); });
                isArticulatedHandModelSetup = true;
                Debug.LogWarning("setup articulated hands - might take a while");
            }
        }
    }

    private void ToggleRay(bool val)
    {
        rayInteractor.SetActive(val);
    }

}
