using _Project.Runtime.Meta;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories.UI
{
    public class HintViewFactory : IHintViewFactory, IInitializable
    {
        private const string ViewPath = "UI/HintView";
        private readonly IAssetManager _assetManager;
        private readonly IInstantiator _instantiator;
        private Object _prefab;
        
        public void Initialize()
        {
            _prefab = _assetManager.Get(ViewPath);
        }

        public HintViewFactory(IInstantiator instantiator, IAssetManager assetManager)
        {
            _instantiator = instantiator;
            _assetManager = assetManager;
        }

        public IView Create(Transform parent)
        {
            return _instantiator.InstantiatePrefabForComponent<HintView>(_prefab, parent);
        }
    }

    public interface IHintViewFactory
    {
        public IView Create(Transform parent);
    }
}