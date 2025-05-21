using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DissolveAnim : MonoBehaviour
{ 
    [Header("Dissolve Settings")]
    [Tooltip("MinTreshhold")]
    public float MinTreshhold = -0.05f;

    [Tooltip("MaxTreshhold")]
    public float MaxTreshhold = 1.01f;

    [Tooltip("AnimationSpeed")]
    public float AnimationSpeed = 0.5f;
    private const string TresholdKey = "_Edge";
    
    [SerializeField] public GameObject _obj;
    [SerializeField] public UnityEvent _action;
    private MeshRenderer _renderer;
    private Material _dissolveMaterial;
    private Coroutine _show;

    private void Awake()
    {
        
        _renderer = _obj.GetComponent<MeshRenderer>();
        _dissolveMaterial = _renderer.sharedMaterials[0];
        
        if(_dissolveMaterial == null)
            Debug.LogError("Dissolve material not found!");
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            StartDissolveCoroutineAppear();
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            StartDissolveCoroutineDissolve();
        }
    }

    public void StartDissolveCoroutineAppear()
    {
        if(_show != null)
            StopCoroutine(_show);
        
        _show = StartCoroutine(Appear());
    }
    public void StartDissolveCoroutineDissolve()
    {
        if(_show != null)
            StopCoroutine(_show);
        
        _show = StartCoroutine(Dissolve());
    }

    public IEnumerator Dissolve()
    {
        float treshold = _dissolveMaterial.GetFloat(TresholdKey);
        while (treshold < MaxTreshhold)
        {
            treshold += Time.deltaTime * AnimationSpeed;
            _dissolveMaterial.SetFloat(TresholdKey, treshold);
            yield return null;
        }
        _dissolveMaterial.SetFloat(TresholdKey, MaxTreshhold);
        _action?.Invoke();
    }
    
    public IEnumerator Appear()
    {
        float treshold = _dissolveMaterial.GetFloat(TresholdKey);
        while (treshold >= MinTreshhold)
        {
            treshold -= Time.deltaTime * AnimationSpeed;
            _dissolveMaterial.SetFloat(TresholdKey, treshold);
            yield return null;
        }
        _dissolveMaterial.SetFloat(TresholdKey, MinTreshhold);
        _action?.Invoke();
    }
    
    private void OnDestroy()
    {
        if(_dissolveMaterial != null)
            _dissolveMaterial.SetFloat(TresholdKey, MinTreshhold);
    }
}