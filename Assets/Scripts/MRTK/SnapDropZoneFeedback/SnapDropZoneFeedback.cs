using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;

public class SnapDropZoneFeedback : FeedbackInteraction
{
    private SnapDropZone _snapDropZone;

    private Outline _outline;
    private OutlineHierarchy _outlineHierarchy;
    
    [Header("Renderer in hierarchy")]
    [Tooltip("If this is enabled, the script get all renderer in the object hierarchy.")]
    public bool getRendererInHierarchy;
    
    private Color32 _targetColor;
    private Color32 _targetFadeOutColor;
    
    private Color32 _grabbedInvincibleColor = new Color32(0, 107, 255, 0);
    public Color32 grabbedColor = new Color32(0, 107, 255, 255);
    
    [Header("Fade speed")]    
    public float fadeSpeed = 2.0f;
    private Coroutine _fadeInCoroutine;
    private Coroutine _fadeOutCoroutine;
    private float _timePassed;
    
    protected override void Init()
    {
        //base.Init();
        _targetColor = grabbedColor;
        _targetFadeOutColor = _grabbedInvincibleColor;
        
        if (gameObject.TryGetComponent(out SnapDropZone snapZone))
        {
            _snapDropZone = snapZone;
        }

        if (getRendererInHierarchy)
        {
            if (!gameObject.TryGetComponent(out OutlineHierarchy objHierarchyOutline))
            {
                _outlineHierarchy = gameObject.AddComponent<OutlineHierarchy>();
                _outlineHierarchy.OutlineMode = OutlineHierarchy.Mode.OutlineAll;
                _outlineHierarchy.OutlineColor = _targetFadeOutColor;
                _outlineHierarchy.OutlineWidth = 10f;
                _outlineHierarchy.DisableAllMaterial();
            }
        }
        else
        {
            if (!gameObject.TryGetComponent(out Outline objOutline))
            {
                _outline = gameObject.AddComponent<Outline>();
                _outline.OutlineMode = Outline.Mode.OutlineAll;
                _outline.OutlineColor = _targetFadeOutColor;
                _outline.OutlineWidth = 10f;
                _outline.DisableAllMaterial();
            }
        }

        if (_snapDropZone.snapDropObject.TryGetComponent(out MRTKBaseInteractable baseInteractable))
        {
            //Grabbed
            baseInteractable.IsGrabSelected.OnEntered.AddListener(OnIsGrabSelected);
            baseInteractable.IsGrabSelected.OnExited.AddListener(OnIsGrabUnselected);
        }
        else
        {
            Debug.Log(transform.name + ": StatefulInteracable not found!");
        }
    }

    protected override void OnIsGrabSelected(float args)
    {
        base.OnIsGrabSelected(args);
        if (_fadeOutCoroutine != null)
            StopCoroutine(_fadeOutCoroutine);

        if(_outline != null)
            _outline.EnableAllMaterial();
        if(_outlineHierarchy != null)
            _outlineHierarchy.EnableAllMaterial();
        
        _snapDropZone.ActivateAllRenderer();
        _fadeInCoroutine = StartCoroutine(FadeIn());
    }

    protected override void OnIsGrabUnselected(float args)
    {
        base.OnIsGrabUnselected(args);
        if(_fadeInCoroutine != null)
            StopCoroutine(_fadeInCoroutine);
            
        _fadeOutCoroutine = StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeIn()
    {
        Color initialColor = Color.magenta;

        if (_outline != null)
            initialColor = _outline.OutlineColor;
        if (_outlineHierarchy != null)
            initialColor = _outlineHierarchy.OutlineColor;
        
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            
            if(_outline != null)
                _outline.OutlineColor = Color.Lerp(initialColor, _targetColor, elapsedTime / fadeSpeed);
            if(_outlineHierarchy != null)
                _outlineHierarchy.OutlineColor = Color.Lerp(initialColor, _targetColor, elapsedTime / fadeSpeed);
            yield return null;
        }
    }
    
    private IEnumerator FadeOut()
    {
        Color initialColor = Color.magenta;
        if(_outline != null)
            initialColor = _outline.OutlineColor;
        if(_outlineHierarchy != null)
            initialColor = _outlineHierarchy.OutlineColor;
        
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            
            if(_outline != null)
                _outline.OutlineColor = Color.Lerp(initialColor, _targetFadeOutColor, elapsedTime / fadeSpeed);
            if(_outlineHierarchy != null)
                _outlineHierarchy.OutlineColor = Color.Lerp(initialColor, _targetFadeOutColor, elapsedTime / fadeSpeed);
            yield return null;
        }
    }
}
