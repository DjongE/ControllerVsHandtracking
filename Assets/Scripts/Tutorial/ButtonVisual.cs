using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum ButtonType
{
    primary,
    secondary,
    trigger,
    grip,
    thumbstick,
    menu
}

public class ButtonVisual : MonoBehaviour
{
    public ButtonType buttonType = ButtonType.primary;

    public UnityEvent<InputAction.CallbackContext> OnButtonClicked;

    private Renderer visualRenderer;
    private ToolTipConnector toolTip;
    private ParticleSystem ploppParticles;

    private void Awake()
    {
        visualRenderer = GetComponent<MeshRenderer>();
        ploppParticles = GetComponent<ParticleSystem>();
        toolTip = GetComponentInChildren<ToolTipConnector>(true);

        OnButtonClicked.AddListener(delegate { HideVisual(); });
    }

    // Start is called before the first frame update
    void Start()
    {
        //visualRenderer.enabled = false;
    }

    public void ShowVisual()
    {
        Debug.Log("show");
        visualRenderer.enabled = true;
        toolTip.gameObject.SetActive(true);
    }

    public void HideVisual()
    {
        Debug.Log("hide");
        visualRenderer.enabled = false;
        toolTip.gameObject.SetActive(false);
        ploppParticles.Play();
    }
}
