using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ToolTipConnector : MonoBehaviour
{
    public Transform target;
    [Tooltip("Draw the line with an offset to this GameObject's origin")]
    public Vector3 offset = new Vector3(0, -0.05f, 0);

    public GameObject labelPlate;

    private Vector3 _targetPos;

    private LineRenderer _line;

    

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the line so that it always points at the target
        if (target)
        {
            _targetPos = target.position;
        }
        else
        {
            _targetPos = new Vector3(0, -1, 0);
        }

        _line.SetPosition(0, gameObject.transform.position + offset);
        _line.SetPosition(1, _targetPos);

        // Rotate the label so that it always faces the user
        labelPlate.transform.LookAt(Camera.main.transform);
    }

    private void DrawLine()
    {

    }
}
