using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    public ArticulatedHandController articulatedHandController;

    public UnityEvent OnControllerModelSpawned;
    public UnityEvent OnHandModelSpawned;

    public InteractionHandler interactionHandler;
    
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    private GameObject articulatedHandModel;
    private SkinnedMeshRenderer articulatedHandRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if(interactionHandler.controller)
            TryInitialize();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name.Contains(targetDevice.name));
            if (prefab)
            {
                if (!spawnedController)
                {
                    spawnedController = Instantiate(prefab, transform);

                    // Notify game that controller did spawn and is available
                    OnControllerModelSpawned.Invoke();
                }
            }
            else
            {
                Debug.Log("Did not find corresponding controller model");
            }

            if (!spawnedHandModel)
            {
                spawnedHandModel = Instantiate(handModelPrefab, transform);
                handAnimator = spawnedHandModel.GetComponent<Animator>();
            }
        }
    }
    
    public GameObject GetHandPrefab()
    {
        return spawnedHandModel;
    }

    void UpdateHandAnimation()
    {
        if (articulatedHandController.activateActionValue.action.ReadValue<float>() > 0.0f)
        {
            handAnimator.SetFloat("Trigger", articulatedHandController.activateActionValue.action.ReadValue<float>());
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }


        if (articulatedHandController.selectActionValue.action.ReadValue<float>() > 0.0f )
        {
            handAnimator.SetFloat("Grip", articulatedHandController.selectActionValue.action.ReadValue<float>());
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }

        //if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        //{
        //    handAnimator.SetFloat("Trigger", triggerValue);
        //}
        //else
        //{
        //    handAnimator.SetFloat("Trigger", 0);
        //}

        //if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        //{
        //    handAnimator.SetFloat("Grip", gripValue);
        //}
        //else
        //{
        //    handAnimator.SetFloat("Grip", 0);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!articulatedHandModel && articulatedHandController.model)
        {
            articulatedHandModel = articulatedHandController.model.gameObject;
            articulatedHandRenderer = articulatedHandModel.GetComponentInChildren<SkinnedMeshRenderer>();
            Debug.LogWarning("setup articulated hands - might take a while");
        }

        if (articulatedHandRenderer && articulatedHandRenderer.enabled)
        {
            // Articulated hand model is shown, so hide our controller hand model
            if (spawnedHandModel)
                spawnedHandModel.SetActive(false);
            if (spawnedController)
                spawnedController.SetActive(false);

            return;
        }

        if (!targetDevice.isValid && interactionHandler.controller)
        {
            TryInitialize();
        }
        
        else
        {
            if (showController)
            {
                if (spawnedHandModel)
                    spawnedHandModel.SetActive(false);
                if (spawnedController)
                    spawnedController.SetActive(true);
            }
            else
            {
                if (spawnedHandModel)
                    spawnedHandModel.SetActive(true);
                if (spawnedController)
                    spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }
}
