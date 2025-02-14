using _Project.Runtime.Config;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class PotionApplierFactory : IPotionApplierFactory
    {
        
        protected readonly IGrenadeConfig _grenadeConfig;
        protected readonly IAssetManager _assetManager;
        protected readonly IInstantiator _instantiator;
        protected readonly IPotionExplosionProvider _potionsExplosionProvider;
        protected readonly IHealth _health;


        public PotionApplierFactory(
            IGrenadeConfig grenadeConfig,
            IAssetManager assetManager,
            IInstantiator instantiator,
            IPotionExplosionProvider iPotionExplosionProvider,
            IHealth health
            )
        {
            _grenadeConfig = grenadeConfig;
            _assetManager = assetManager;
            _instantiator = instantiator;
            _potionsExplosionProvider = iPotionExplosionProvider;
            _health = health;
        }

        public void ApplyWorldChange(Vector3 mousePosition, Vector3 worldPosition)
        {
            var grende = new PotionWorldChange(
                _grenadeConfig,
                _assetManager,
                _instantiator,
                mousePosition,
                worldPosition,
                _potionsExplosionProvider.RemoveExplosionFromListAndUpdateTrigger,
                _potionsExplosionProvider.AddExplosionToListAndUpdateTrigger);
            grende.MakeAction();
        }

        public void ApplyHealing(Vector3 mousePosition, Vector3 worldPosition)
        {
            var grende = new PotionHeal(
                _grenadeConfig,
                _assetManager,
                _instantiator,
                _health,
                mousePosition,
                worldPosition);
            grende.MakeAction();
        }
        
    }
    
    public interface IPotionApplierFactory
    {
        public void ApplyWorldChange(Vector3 mousePosition, Vector3 worldPosition);
        public void ApplyHealing(Vector3 mousePosition, Vector3 worldPosition);
    }
    
    
}