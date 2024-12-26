using _Project.Runtime.InventorySystem;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class FlowerFactory : IFlowerFactory
    {
        private readonly IAssetManager _assetManager;
        private readonly IInstantiator _instantiator;
        public FlowerFactory(IInstantiator instantiator, IAssetManager assetManager)
        {
            _instantiator = instantiator;
            _assetManager = assetManager;
        }

        public Flower Create(ItemEnum type)
        {
            var prefab = _assetManager.Get($"Prefabs/{type}");
            return _instantiator.InstantiatePrefabForComponent<Flower>(prefab);
        }
    }
}