using _Project.Runtime.Core.Enemies;
using ObservableCollections;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory, IInitializable
    {
        private const string EnemyPath = "Birch";

        private readonly IAssetManager _assetManager;
        private readonly IInstantiator _instantiator;
        private GameObject _prefab;

        public EnemyFactory(IInstantiator instantiator, IAssetManager assetManager)
        {
            _instantiator = instantiator;
            _assetManager = assetManager;
        }

        public Enemy Create(Vector3 at)
        {
            return _instantiator.InstantiatePrefabForComponent<Birch>(_prefab, at, Quaternion.identity, null);
        }

        public void Initialize()
        {
            _prefab = _assetManager.Get(EnemyPath);
        }
    }

    public interface IEnemyFactory
    {
        public Enemy Create(Vector3 at);
    }

    public interface IEnemiesProvider
    {
        public IObservableCollection<Enemy> Enemies { get; }
    }
}


