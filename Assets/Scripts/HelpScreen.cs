using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Used to show and hide the help screen when controllers are used.
/// </summary>
public class HelpScreen : MonoBehaviour
{

    [SerializeField]
    public InputActionReference showHelp;
    private InputAction _showHelp;

    public GameObject objectToHide;

    public UnityEvent<InputAction.CallbackContext> OnHelpShown;
    public UnityEvent<InputAction.CallbackContext> OnHelpHidden;

    private void Awake()
    {
        _showHelp = showHelp.action;
    }

    // Start is called before the first frame update
    void Start()
    {

        _showHelp.started += OnHelpShown.Invoke;
        _showHelp.canceled += OnHelpHidden.Invoke;

        OnHelpShown.AddListener(delegate { ToggleHelpScreen(true); });
        OnHelpHidden.AddListener(delegate { ToggleHelpScreen(false); });

        objectToHide.SetActive(false);

    }

    private void OnDestroy()
    {
        _showHelp.started -= OnHelpShown.Invoke;
        _showHelp.canceled -= OnHelpHidden.Invoke;

        OnHelpShown.RemoveListener(delegate { ToggleHelpScreen(true); });
        OnHelpHidden.RemoveListener(delegate { ToggleHelpScreen(false); });
    }

    private void ToggleHelpScreen(bool val)
    {
        objectToHide.SetActive(val);
    }
}
