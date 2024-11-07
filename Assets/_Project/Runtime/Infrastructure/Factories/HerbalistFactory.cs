using _Project.Runtime.Core.Herbalist;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories
{
    public sealed class HerbalistFactory : IHerbalistProvider
    {
        private const string HerbalistPath = "Herbalist";

        private readonly IAssetManager _assetManager;
        private readonly IInstantiator _instantiator;
        public HerbalistFactory(IInstantiator instantiator, IAssetManager assetManager)
        {
            _instantiator = instantiator;
            _assetManager = assetManager;
        }

        public IHerbalist Herbalist { get; private set; }

        public void Create()
        {
            GameObject prefab = _assetManager.Get(HerbalistPath);
            Herbalist = _instantiator.InstantiatePrefabForComponent<Herbalist>(prefab);
        }
    }

    public interface IHerbalistProvider
    {
        public IHerbalist Herbalist { get; }
    }
}