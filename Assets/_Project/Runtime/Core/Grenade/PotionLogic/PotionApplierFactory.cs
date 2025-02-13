using _Project.Runtime.Config;
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


        public PotionApplierFactory(
            IGrenadeConfig grenadeConfig,
            IAssetManager assetManager,
            IInstantiator instantiator,
            IPotionExplosionProvider iPotionExplosionProvider
            )
        {
            _grenadeConfig = grenadeConfig;
            _assetManager = assetManager;
            _instantiator = instantiator;
            _potionsExplosionProvider = iPotionExplosionProvider;
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
        
    }
    
    public interface IPotionApplierFactory
    {
        public void ApplyWorldChange(Vector3 mousePosition, Vector3 worldPosition);
    }
    
    
}