using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Runtime.Core.Grenades;
using _Project.Runtime.Core.Grenades.GlobalWorldChange;
using _Project.Runtime.Core.Grenades.PotionLogic;
using _Project.Runtime.Core.Herbalist;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class AnotherDimension : MonoBehaviour
{
    private Material _material;
    //[SerializeField] private int _radius = 2;

    private readonly Vector4[] _grenadesPositions = new Vector4[15];
    private int _grenadeCount;
    private IPotionExplosionProvider _provider;
    private IGlobalWorldChangeSubscriptable _globalWorldChange;
    private static readonly int NumberGrenade = Shader.PropertyToID("_NumberGrenade");
    private static readonly int GrenadesPositions = Shader.PropertyToID("_GrenadesPositions");
    private static readonly int Dist = Shader.PropertyToID("_Dist");

    [Inject]
    private void Construct(IPotionExplosionProvider provider, IGlobalWorldChangeSubscriptable globalWorldChange)
    {
        _provider = provider;
        _globalWorldChange = globalWorldChange;
    }

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        _provider.GrenadesUpdated += OnGrenadesUpdated;
        _globalWorldChange.OnGlobalWorldChange += OnGlobalWorldChange;
    }

    private void OnGrenadesUpdated(IReadOnlyList<GrenadeExplosion> grenadeExplosions)
    {
        for (int i = 0; i < grenadeExplosions.Count; i++)
        {
            _grenadesPositions[i] = grenadeExplosions[i].transform.position;
        }
        
        var radius = grenadeExplosions.Count > 0 ?grenadeExplosions.Select(e => e.Radius).Max() : 1;

        _material.SetInt(NumberGrenade, grenadeExplosions.Count);
        _material.SetVectorArray(GrenadesPositions, _grenadesPositions);
        _material.SetFloat(Dist, radius);
    }

    private void OnGlobalWorldChange(GrenadeExplosion explosion)
    {
        if (explosion is null)
        {
            OnGrenadesUpdated(new List<GrenadeExplosion>());
        }
        else
        {
            OnGrenadesUpdated(new List<GrenadeExplosion>{explosion});
        }
    }

    private void OnDisable()
    {
        _provider.GrenadesUpdated -= OnGrenadesUpdated;
        _globalWorldChange.OnGlobalWorldChange -= OnGlobalWorldChange;
    }
}