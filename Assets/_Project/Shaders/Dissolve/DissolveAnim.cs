using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DissolveAnim : MonoBehaviour
{ 
    [Header("Dissolve Settings")]
    [Tooltip("MinTreshhold")]
    public float MinTreshhold = -0.2f;

    [Tooltip("MaxTreshhold")]
    public float MaxTreshhold = 1.01f;

    [Tooltip("AnimationSpeed")]
    public float AnimationSpeed = 0.5f;
    private const string TresholdKey = "_Edge";
    
    private MeshRenderer _renderer;
    private Material _dissolveMaterial;
    private Coroutine _show;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _dissolveMaterial = _renderer.sharedMaterials[0];
        
        if(_dissolveMaterial == null)
            Debug.LogError("Dissolve material not found!");
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            StartDissolveCoroutine(Dissolve());
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            StartDissolveCoroutine(Appear());
        }
    }

    private void StartDissolveCoroutine(IEnumerator routine)
    {
        if(_show != null)
            StopCoroutine(_show);
        
        _show = StartCoroutine(routine);
    }

    private IEnumerator Dissolve()
    {
        float treshold = _dissolveMaterial.GetFloat(TresholdKey);
        while (treshold < MaxTreshhold)
        {
            treshold += Time.deltaTime * AnimationSpeed;
            _dissolveMaterial.SetFloat(TresholdKey, treshold);
            yield return null;
        }
        _dissolveMaterial.SetFloat(TresholdKey, MaxTreshhold);
    }
    
    private IEnumerator Appear()
    {
        float treshold = _dissolveMaterial.GetFloat(TresholdKey);
        while (treshold >= MinTreshhold)
        {
            treshold -= Time.deltaTime * AnimationSpeed;
            _dissolveMaterial.SetFloat(TresholdKey, treshold);
            yield return null;
        }
        _dissolveMaterial.SetFloat(TresholdKey, MinTreshhold);
    }
    
    private void OnDestroy()
    {
        if(_dissolveMaterial != null)
            _dissolveMaterial.SetFloat(TresholdKey, MinTreshhold);
    }
}