using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerTutorial : MonoBehaviour
{
    public DataCollector dataCollector;
    public UnityEvent startControllerInteractionEvent;

    public GameObject channel1ControllerDisplay;
    public GameObject channel3InteractionDisplay;

    public List<GameObject> controllerTutorialDisplay;

    private void Start()
    {
        channel1ControllerDisplay.SetActive(true);
    }

    public void StartControllerInteractions()
    {
        dataCollector.dataName = "Controller";
        StartCoroutine(ControllerTutorialIsDone());
        controllerTutorialDisplay[0].SetActive(false);
        controllerTutorialDisplay[1].SetActive(true);
    }

    private IEnumerator ControllerTutorialIsDone()
    {
        yield return new WaitForSeconds(3f);
        startControllerInteractionEvent.Invoke();
        controllerTutorialDisplay[1].SetActive(false);
        channel3InteractionDisplay.SetActive(true);
    }
}
