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
    private IPotionExplosionProvider _provider;
    private static readonly int NumberGrenade = Shader.PropertyToID("_NumberGrenade");
    private static readonly int GrenadesPositions = Shader.PropertyToID("_GrenadesPositions");
    private static readonly int Dist = Shader.PropertyToID("_Dist");

    [Inject]
    private void Construct(IPotionExplosionProvider provider)
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

    private void OnGrenadesUpdated(IReadOnlyList<GrenadeExplosion> grenadeExplosions)
    {
        for (int i = 0; i < grenadeExplosions.Count; i++)
        {
            _grenadesPositions[i] = grenadeExplosions[i].transform.position;
        }

        _material.SetInt(NumberGrenade, grenadeExplosions.Count);
        _material.SetVectorArray(GrenadesPositions, _grenadesPositions);
        _material.SetFloat(Dist, _radius);
    }

    private void OnDisable()
    {
        _provider.GrenadesUpdated -= OnGrenadesUpdated;
    }
}