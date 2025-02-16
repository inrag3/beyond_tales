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
        protected readonly IPoisoningProvider _poisoningProvider;


        public PotionApplierFactory(
            IGrenadeConfig grenadeConfig,
            IAssetManager assetManager,
            IInstantiator instantiator,
            IPotionExplosionProvider iPotionExplosionProvider,
            IHealth health,
            IPoisoningProvider poisoningProvider
            )
        {
            _grenadeConfig = grenadeConfig;
            _assetManager = assetManager;
            _instantiator = instantiator;
            _potionsExplosionProvider = iPotionExplosionProvider;
            _health = health;
            _poisoningProvider = poisoningProvider;
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

        public void ApplyExplosion(Vector3 mousePosition, Vector3 worldPosition)
        {
            var grende = new ExplosionPotion(
                _grenadeConfig,
                _assetManager,
                _instantiator,
                mousePosition,
                worldPosition);
            grende.MakeAction();
        }

        public void ApplyPoison(Vector3 mousePosition, Vector3 worldPosition)
        {
            var grende = new PoisonPotion(
                _grenadeConfig,
                _assetManager,
                _instantiator,
                mousePosition,
                worldPosition,
                _poisoningProvider.StartPoisoning
                );
            grende.MakeAction();
        }
        
        
    }
    
    public interface IPotionApplierFactory
    {
        public void ApplyWorldChange(Vector3 mousePosition, Vector3 worldPosition);
        public void ApplyHealing(Vector3 mousePosition, Vector3 worldPosition);
        public void ApplyExplosion(Vector3 mousePosition, Vector3 worldPosition);
        public void ApplyPoison(Vector3 mousePosition, Vector3 worldPosition);
    }
    
    
}