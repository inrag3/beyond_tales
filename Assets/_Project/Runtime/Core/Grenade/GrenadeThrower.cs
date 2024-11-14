using System;
using System.Collections.Generic;
using _Project.Runtime.Config;
using _Project.Runtime.Infrastructure;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class GrenadeThrower : ITickable, IDisposable, IGrenadeProvider
    
    {
        private const string GrenadePath = "Granade";
        private const string ExplosionPath = "ExplosionCenter";

        private bool _readyToThrow = true;


        private readonly IHerbalistProvider _herbalistProvider;
        private readonly IInputService _inputService;

        private readonly IAssetManager _assetManager;
        private readonly IInstantiator _instantiator;
        private readonly IGrenadeConfig _grenadeConfig;
        private readonly Timer _timer;

        public GrenadeThrower(
            IHerbalistProvider herbalistProvider,
            IInputService inputService,
            IInstantiator instantiator,
            IAssetManager assetManager,
            IGrenadeConfig grenadeConfig,
            Timer timer
        )
        {
            _herbalistProvider = herbalistProvider;
            _inputService = inputService;
            _instantiator = instantiator;
            _assetManager = assetManager;
            _grenadeConfig = grenadeConfig;
            _timer = timer;
            timer.TimeEnded += ResetThrow;
        }


        public void Tick()
        {
            if (!(_inputService.IsGrenadeButtonPressed && _readyToThrow))
                return;
            _readyToThrow = false;

            GameObject prefab = _assetManager.Get(GrenadePath);
            var grenade = _instantiator.InstantiatePrefabForComponent<Grenade>(prefab);
            grenade.transform.position += Vector3.up;
            grenade.transform.parent = null;

            Rigidbody rigidbody = grenade.GetComponent<Rigidbody>();

            Vector3 forceToAdd = _herbalistProvider.Herbalist.Transform.forward * _grenadeConfig.GrenadeFrontForce +
                                 prefab.transform.up * _grenadeConfig.GrenadeUpForce;
            rigidbody.AddForce(forceToAdd, ForceMode.Impulse);

            grenade.Hit += GrenadeContactCallback;

            _timer.Start(_grenadeConfig.GrenadeThrowsTimeout);
        }

        private void GrenadeContactCallback(Grenade grenade)
        {
            Debug.Log("Grenade contact: " + grenade.transform.position);

            GameObject prefab = _assetManager.Get(ExplosionPath);
            var explosion = _instantiator.InstantiatePrefabForComponent<GrenadeExplosion>(prefab);
            explosion.transform.parent = grenade.transform.parent;
            explosion.transform.position = grenade.transform.position;
            
            Grenades.Add(grenade);
            GrenadesUpdated?.Invoke();

        }

        private void ResetThrow()
        {
            _readyToThrow = true;
        }

        public void Dispose()
        {
            _timer.TimeEnded -= ResetThrow;
        }

        public  List<Grenade> Grenades { get; private set; } = new  List<Grenade>();
        
        public event Action GrenadesUpdated;
    }

    public interface IGrenadeProvider
    {
        
        public List<Grenade> Grenades { get; }

        public event Action GrenadesUpdated;
        
    }
}