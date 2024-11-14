using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Runtime.Core.Herbalist;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class AnotherDimension : MonoBehaviour
{
    private Material _material;
    [SerializeField] private int _radius = 2;
    
    private readonly Vector4[] _grenadesPositions = new Vector4[15];
    private int _grenadeCount;
    private IGrenadeProvider _provider;
    private static readonly int NumberGrenade = Shader.PropertyToID("_NumberGrenade");
    private static readonly int GrenadesPositions = Shader.PropertyToID("_GrenadesPositions");
    private static readonly int Dist = Shader.PropertyToID("_Dist");

    [Inject] 
    private void Construct(IGrenadeProvider provider)
    {
        _provider = provider;
    }
    
    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }
    
    private void OnEnable()
    {
        _provider.GrenadesUpdated += OnGrenadesUpdated;
    }

    private void OnGrenadesUpdated()
    {
        var grenades = _provider.Grenades;
        if (grenades.Count > 0)
        {
           
            for (int i = 0; i < grenades.Count; i++)
            {
                _grenadesPositions[i] = grenades[i].transform.position;
            }
        
            _material.SetInt(NumberGrenade, grenades.Count);
            _material.SetVectorArray(GrenadesPositions, _grenadesPositions);
            _material.SetFloat(Dist, _radius);
        }
    }
    
    private void OnDisable()
    {
        _provider.GrenadesUpdated -= OnGrenadesUpdated;
    }
}
