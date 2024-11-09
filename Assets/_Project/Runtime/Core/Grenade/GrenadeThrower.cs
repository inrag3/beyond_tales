using _Project.Runtime.Infrastructure;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class GrenadeThrower :ITickable
    {
        private const string GrenadePath = "Granade";
        
        private const float ForwardSpeed = 1.0f;
        private const float UpSpeed = 0.0f;
        
        private const float ThrowCooldown = 2.0f;
        
        private bool _readyToThrow = true;
        

        
        private readonly IHerbalistProvider _herbalistProvider;
        private readonly IInputService _inputService;

        private readonly IAssetManager _assetManager;
        private readonly IInstantiator _instantiator;

        public GrenadeThrower(
            IHerbalistProvider herbalistProvider,
            IInputService inputService,
            IInstantiator instantiator,
            IAssetManager assetManager
        )
        {
            _herbalistProvider = herbalistProvider;
            _inputService = inputService;
            _instantiator = instantiator;
            _assetManager = assetManager;
        }

        public void Tick()
        {
            if(!_inputService.IsGrenadeButtonPressed && _readyToThrow)
                return;
            //_readyToThrow = false;
            
            GameObject prefab = _assetManager.Get(GrenadePath);
            var grenade = _instantiator.InstantiatePrefabForComponent<Grenade>(prefab);
            grenade.transform.position += Vector3.up;
            grenade.transform.parent = _herbalistProvider.Herbalist.Transform.parent;
            
            Rigidbody rigidbody = grenade.GetComponent<Rigidbody>();

            Vector3 forceToAdd = _herbalistProvider.Herbalist.Transform.forward * ForwardSpeed + prefab.transform.up * UpSpeed;
            rigidbody.AddForce(forceToAdd, ForceMode.Impulse);
            
            
            Debug.Log("Grenade Thrower "+ _herbalistProvider.Herbalist.Transform.position.ToString() );
            
            //Invoke(nameof(ResetThrow), ThrowCooldown);
        }
        
        private void ResetThrow()
        {
            _readyToThrow = true;
        }
    }
}