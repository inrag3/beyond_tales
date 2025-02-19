using _Project.Runtime.Meta.Health;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories.UI
{
    public class HealthViewFactory : IHealthViewFactory, IInitializable
    {
        private const string ViewPath = "UI/HealthView";

        private readonly IAssetManager _assetManager;
        private readonly IInstantiator _instantiator;
        private Object _prefab;
        
        public void Initialize()
        {
            _prefab = _assetManager.Get(ViewPath);
        }

        public HealthViewFactory(IInstantiator instantiator, IAssetManager assetManager)
        {
            _instantiator = instantiator;
            _assetManager = assetManager;
        }

        public HealthView Create(Transform parent)
        {
            return _instantiator.InstantiatePrefabForComponent<HealthView>(_prefab, parent);
        }
    }
}