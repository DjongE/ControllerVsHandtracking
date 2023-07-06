using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class SnapDropZone : MonoBehaviour
{
    [Header("Snap Drop Object")]
    [Tooltip("The object to be snapped.")]
    public GameObject snapDropObject;
    private ObjectManipulator _objectManipulator;
    private GameObject _colliderObject;
    
    private ArrayList _allChildGameObjectsOldPosition;
    private int _childIndex;

    [Header("Snap Drop Object over tag")]
    [Tooltip("If this option is enabled, you must reference a prefab as the snapDropObject.")]
    public bool identifyObjectOverTag;
    [HideInInspector]
    public string selectedTag = "";

    [Header("Virtual Object Material")]
    [Tooltip("The material, how the virtual object is to be displayed.")][Space(30)]
    public Material virtualObjectMaterial;
    public Material virtualTransparentMaterial;
    
    [Header("Virtual Object Container")]
    [Tooltip("The place where the virtual object is generated.")]
    public GameObject virtualObjectContainer;

    private Transform _snapObjectContainer;
    private Transform _snapDropObjectOldParent;
    private Vector3 _unSnapScale;

    private SphereCollider _sphereCollider;
    
    //Virtual child list
    private int _virtualObjectChild;
    private List<Transform> _virtualChildObjectList;

    [Header("Settings")]
    [Tooltip("True: The snapped object can be removed from the SnapDropZone.")]
    public bool grabableAfterSnap;
    
    [Tooltip("True: This is useful if the SnapDropZone has a different scale than the object to be snapped. As soon as the original object is snapped, the scale is adjusted.")]
    public bool scaleAfterSnap;

    private bool _isCollided;
    
    private GameObject _virtualObject;
    protected ArrayList virtualObjectRendererList;
    
    private GameObject _selfGameObject;
    private Transform _selfTransform;

    [Header("Object is snappable")]
    [Tooltip("True: The object can be placed in the SnapDropZone.")]
    public bool isSnapable;
    
    [Header("Information")]
    [Tooltip("True: An Object is snapped to the SnapDropZone.")]
    public bool isSnapped;
    [Tooltip("This event is executed when the original object has been snapped.")]
    public UnityEvent onSnapEvent;
    [Tooltip("This event is executed when the original object has been unsnapped.")]
    public UnityEvent onUnsnapEvent;
    public UnityEvent onTriggerEnterEvent;
    public UnityEvent onTriggerExitEvent;
    
    private OnChangeBoolEvent _boolEvent;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _boolEvent = GetComponent<OnChangeBoolEvent>();
        _boolEvent.OnVariableChange += IsSnappedChanged;
        
        _sphereCollider = GetComponent<SphereCollider>();
        _allChildGameObjectsOldPosition = new ArrayList();
        virtualObjectRendererList = new ArrayList();
        _virtualChildObjectList = new List<Transform>();
        
        if (snapDropObject != null)
        {
            _snapDropObjectOldParent = snapDropObject.transform.parent;
            _unSnapScale = snapDropObject.transform.localScale;

            GetAllChildOldPosition();
        }
        
        _snapObjectContainer = transform.GetChild(1);

        _selfGameObject = gameObject;
        _selfTransform = transform;

        GenerateVirtualObject();

        if (_virtualObject == null)
            _virtualObject = GetVirtualObject();
        
        DisableObjectBoundsControl();
        DisableMenuAttach();

        if(virtualObjectRendererList.Count <= 0 && _virtualObject != null)
            GetAllRenderer();
        
        if(virtualObjectRendererList.Count > 0)
            DeactivateAllRenderer();


        if (_virtualObject != null)
        {
            ChangeVirtualObjectMaterial(virtualTransparentMaterial);
        }
    }

    private void Awake()
    {
        if(!gameObject.TryGetComponent(out SnapDropZoneFeedback snapDropFeedback))
            gameObject.AddComponent<SnapDropZoneFeedback>();
    }

    public void IsSnappedChanged(bool newval)
    {
        if (newval)
        {
            onSnapEvent.Invoke();
        }
        else
        {
            onUnsnapEvent.Invoke();
        }
    }

    /// <summary>
    /// The virtual object is generated in the editor, you dont need to start the scene.
    /// </summary>
    private void Update()
    {
        if (!Application.isPlaying)
        {
            if(virtualObjectRendererList == null)
                virtualObjectRendererList = new ArrayList();

            if (_virtualChildObjectList == null)
                _virtualChildObjectList = new List<Transform>();
            
            if (snapDropObject != null)
            {
                GenerateVirtualObject();
                
                CompareSnapObjectAndVirtualObject();
                
                GetAllRenderer();
                ChangeVirtualObjectMaterial(virtualObjectMaterial);
                ActivateAllRenderer();
            }
            else
            {
                if (transform.GetChild(0).childCount > 0)
                {
                    _virtualObject = transform.GetChild(0).GetChild(0).gameObject;
                    DestroyImmediate(_virtualObject);
                    virtualObjectRendererList.Clear();
                }
            }
        }
        else
        {
            _boolEvent.MyBool = isSnapped;
            //Check if the object is not grabbed anymore
            GetRightObjectManipulator();
            if (_objectManipulator != null)
            {
                if (_objectManipulator.IsGrabSelected)
                {
                    UnPlaceObject();
                }
                else
                {
                    PlaceObject(_colliderObject);
                }
            }
        }
    }

    /// <summary>
    /// The virtual object is generated.
    /// </summary>
    private void GenerateVirtualObject()
    {
        if (snapDropObject != null && !isSnapped)
        {
            if (_virtualObject == null && virtualObjectContainer.transform.childCount < 1)
            {
                _virtualObject = Instantiate(snapDropObject, _selfTransform.position, _selfTransform.rotation, virtualObjectContainer.transform);
                DisableObjectManipulator();
                DisableObjectBoundsControl();
                DisableMenuAttach();
                DisableVirtualObjectCollider();
                DisableVirtualObjectOutline();
            }
        }
    }

    /// <summary>
    /// Disable the ManipulationType of the ObjectManipulator.
    /// To prevent the virtual object from being movable.
    /// </summary>
    private void DisableObjectManipulator()
    {
        if (_virtualObject.TryGetComponent(out ObjectManipulator om1))
            om1.AllowedManipulations = 0;

        foreach (Transform child in _virtualObject.transform)
        {
            if (child.TryGetComponent(out ObjectManipulator om2))
            {
                om2.AllowedManipulations = 0;
            }
        }
    }

    /// <summary>
    /// BoundsControl is deactivated for the virtual object.
    /// </summary>
    private void DisableObjectBoundsControl()
    {
        if (_virtualObject != null)
        {
            if (_virtualObject.TryGetComponent(out BoundsControl bc))
            {
                DestroyImmediate(bc); 
            }
        }
    }

    private void DisableMenuAttach()
    {
        if (_virtualObject != null)
        {
            if (_virtualObject.TryGetComponent(out MenuAttach ma))
            {
                DestroyImmediate(ma);
            }
        }
    }

    private void DisableVirtualObjectCollider()
    {
        if (_virtualObject.TryGetComponent(out BoxCollider coll))
        {
            coll.enabled = false;
        }
    }

    private void DisableVirtualObjectOutline()
    {
        Debug.Log("Outline asdasd");
        if (_virtualObject.GetComponent<Outline>())
        {
            DestroyImmediate(_virtualObject.GetComponent<Outline>());
        }

        if (_virtualObject.GetComponentInChildren<Outline>())
        {
            DestroyImmediate(_virtualObject.GetComponentInChildren<Outline>());
        }
    }

    private void ChangeVirtualObjectMaterial(Material mat)
    {
        if (virtualObjectRendererList.Count > 0)
        {
            foreach (Renderer renderer in virtualObjectRendererList)
            {
                Material[] mats = renderer.sharedMaterials;
                
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = mat;
                    renderer.sharedMaterials = mats;
                }
            }
        }
    }

    /// <summary>
    /// When the original object is snapped, the scale of the object adjusts to the virtual object.
    /// </summary>
    private void ChangeScaleToVirtualObject()
    {
        if (scaleAfterSnap && isSnapped)
        {
            snapDropObject.transform.localScale = _virtualObject.transform.localScale;
        }
    }

    /// <summary>
    /// Get the object postition of the original object.
    /// </summary>
    private void GetAllChildOldPosition()
    {
        if (snapDropObject != null)
        {
            foreach (Transform child in snapDropObject.transform)
            {
                _allChildGameObjectsOldPosition.Add(child.localPosition);
            }
        }
    }

    /// <summary>
    /// Set the object position to the correct position.
    /// </summary>
    private void SetAllChildRightPosition()
    {
        if (snapDropObject != null)
        {
            foreach (Transform child in snapDropObject.transform)
            {
                //child.localPosition = (Vector3)_allChildGameObjectsOldPosition[_childIndex];

                _childIndex++;
            }
        }

        _childIndex = 0;
    }

    /// <summary>
    /// If the snapped object is to be removable from the SnapDropZone.
    /// </summary>
    private void GrabAfterSnap()
    {
        GetRightObjectManipulator();

        if (!grabableAfterSnap)
            _objectManipulator.AllowedManipulations = 0;
        else
            _objectManipulator.AllowedManipulations =
                TransformFlags.Move | TransformFlags.Rotate | TransformFlags.Scale;
    }

    /// <summary>
    /// Destroy the virtual object immediately.
    /// </summary>
    private void DestroyVirtualObject()
    {
        if (snapDropObject == null)
        {
            if (_virtualObject != null)
            {
                DestroyImmediate(_virtualObject);
            }
        }
    }
    
    /// <summary>
    /// Destroy the virtual object immediately.
    /// </summary>
    private void CompareSnapObjectAndVirtualObject()
    {
        
        //if (!snapDropObject.name.Equals(_virtualObject.name))
        if(!snapDropObject == _virtualObject)
        {
            if (_virtualObject != null)
            {
                DestroyImmediate(_virtualObject);
                GenerateVirtualObject();
            }
        }
    }

    /// <summary>
    /// The original object will be placed in the SnapDropZone.
    /// </summary>
    public void PlaceObject(GameObject original)
    {
        if (_isCollided && !isSnapped && isSnapable)
        {
            original.transform.parent = _snapObjectContainer;
            original.transform.localPosition = _virtualObject.transform.localPosition;
            original.transform.localRotation = _virtualObject.transform.localRotation;

            SetAllChildRightPosition();
            isSnapped = true;
            
            GrabAfterSnap();
            ChangeScaleToVirtualObject();
            
            _virtualObject.SetActive(false);

            _sphereCollider.enabled = false;
        }
    }

    /// <summary>
    /// The original object is taken from the SnapDropZone.
    /// </summary>
    public void UnPlaceObject()
    {
        if (grabableAfterSnap && isSnapped)
        {
            _virtualObject.SetActive(true);
            
            isSnapped = false;

            if (identifyObjectOverTag)
            {
                _snapObjectContainer.GetChild(0).transform.parent = null;
            }
            else
            {
                if (_snapDropObjectOldParent != null)
                {
                    snapDropObject.transform.parent = _snapDropObjectOldParent;
                    snapDropObject.transform.localScale = _unSnapScale;
                }
                else
                {
                    snapDropObject.transform.parent = null;
                    snapDropObject.transform.localScale = _unSnapScale;
                }
            }
            
            _sphereCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_virtualObject.TryGetComponent(out Outline objOutline))
        {
            objOutline.EnableAllMaterial();
        }
        
        if (identifyObjectOverTag)
        {
            if (other.gameObject.tag.Equals(selectedTag))
            {
                _isCollided = true;
                ActivateAllRenderer();

                _colliderObject = other.gameObject;
                
                onTriggerEnterEvent.Invoke();
            }
        }
        else
        {
            GetRightColliderObject();
            if (other.gameObject == _colliderObject)
            {
                _isCollided = true;
                ActivateAllRenderer();
                
                onTriggerEnterEvent.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (identifyObjectOverTag)
        {
            if (other.gameObject.tag.Equals(selectedTag))
            {
                _isCollided = false;
                DeactivateAllRenderer();
                
                onTriggerExitEvent.Invoke();
            }
        }
        else
        {
            if (other.gameObject == _colliderObject)
            {
                _isCollided = false;
                DeactivateAllRenderer();
                
                onTriggerExitEvent.Invoke();
            }
        }
    }

    public GameObject GetCollidedGameObject()
    {
        return _colliderObject;
    }

    private GameObject GetVirtualObject()
    {
        if(virtualObjectContainer.transform.childCount > 0)
            return virtualObjectContainer.transform.GetChild(0).gameObject;

        return null;
    }

    public void ActivateAllRenderer()
    {
        if (virtualObjectRendererList.Count > 0)
        {
            foreach (Renderer renn in virtualObjectRendererList)
            {
                renn.enabled = true;
            }
        }
    }

    public void DeactivateAllRenderer()
    {
        if (virtualObjectRendererList.Count > 0)
        {
            foreach (Renderer renn in virtualObjectRendererList)
            {
                renn.enabled = false;
            }
        }
    }
    
    private void GetAllRenderer()
    {
        for (int i = 0; i < _virtualObject.GetComponentsInChildren<Renderer>().Length; i++)
        {
            virtualObjectRendererList.Add(_virtualObject.GetComponentsInChildren<Renderer>()[i]);
        }
    }

    private void GetRightColliderObject()
    {
        if (snapDropObject != null)
        {
            if (snapDropObject.TryGetComponent(out Collider snapObjectColl))
            {
                _colliderObject = snapDropObject;
            }
            else
            {
                foreach (Transform child in snapDropObject.transform)
                {
                    if (child.TryGetComponent(out Collider coll))
                    {
                        _colliderObject = child.gameObject;
                    }
                }
            }
        }
    }

    private void GetRightObjectManipulator()
    {
        if (identifyObjectOverTag)
        {
            if (_colliderObject != null)
            {
                if (_colliderObject.TryGetComponent(out ObjectManipulator om1))
                {
                    _objectManipulator = om1;
                }
                else
                {
                    _objectManipulator = _colliderObject.GetComponentInChildren<ObjectManipulator>();
                }
            }
        }
        else
        {
            if (snapDropObject != null)
            {
                if (snapDropObject.TryGetComponent(out ObjectManipulator om1))
                {
                    _objectManipulator = snapDropObject.GetComponent<ObjectManipulator>();
                }
                else
                {
                    _objectManipulator = GetComponentInChildren<ObjectManipulator>();
                }
            }
        }
    }
}
