using UnityEngine;

[ExecuteAlways]
public class MenuAttach : MonoBehaviour
{
    [Header("Update In Editor")]
    public bool updateInEditor;
    
    [Header("Object")]
    public GameObject target;
    public GameObject menu;
    
    [Header("Offset")]
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;

    private void Start()
    {
        Attach();
    }

    private void Update()
    {
        if (updateInEditor)
            //Attach();

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (target.name.Equals("colostoma_stencil"))
            {
                Transform a = target.GetComponent<Transform>();
                print("Vor Attach: " + a.position);
                Attach();
                print("Nach Attach: " + a.position);
            }
        }
    }

    private void FixedUpdate()
    {
        Attach();
    }

    private void Attach()
    {
        var position = target.transform.position;
        var thumbX = position.x;
        var thumbY = position.y;
        var thumbZ = position.z;

        var offsetX = offsetPosition.x;
        var offsetY = offsetPosition.y;
        var offsetZ = offsetPosition.z;

        var rotation = target.transform.rotation.eulerAngles;
        var thumbRotX = rotation.x;
        var thumbRotY = rotation.y;
        var thumbRotZ = rotation.z;

        var offsetRotX = offsetRotation.x;
        var offsetRotY = offsetRotation.y;
        var offsetRotZ = offsetRotation.z;

        var newMenuPos = new Vector3(thumbX + offsetX, thumbY + offsetY, thumbZ + offsetZ);
        var newMenuRot = new Vector3(offsetRotX, offsetRotY, offsetRotZ);

        menu.transform.SetPositionAndRotation(newMenuPos, Quaternion.Euler(newMenuRot));
    }
}
