using _Project.Runtime.Core.Enemies;
using ObservableCollections;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory, IInitializable
    {
        private const string BirchPath = "Birch";
        private const string HutPath = "Hut";

        private readonly IAssetManager _assetManager;
        private readonly IInstantiator _instantiator;
        private GameObject _birchPrefab;
        private GameObject _hutPrefab;

        public EnemyFactory(IInstantiator instantiator, IAssetManager assetManager)
        {
            _instantiator = instantiator;
            _assetManager = assetManager;
        }

        public Enemy Create(Vector3 at, EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Birch: return _instantiator.InstantiatePrefabForComponent<Birch>(_birchPrefab, at, Quaternion.identity, null);
                case EnemyType.Hut: return _instantiator.InstantiatePrefabForComponent<Hut>(_hutPrefab, at, Quaternion.identity, null);
                default:
                    Debug.LogError($"Unknown enemy type to create!");
                    return null;
            }
        }

        public void Initialize()
        {
            _birchPrefab = _assetManager.Get(BirchPath);
            _hutPrefab = _assetManager.Get(HutPath);
        }
    }

    public interface IEnemyFactory
    {
        public Enemy Create(Vector3 at, EnemyType enemyType);
    }

    public interface IEnemiesProvider
    {
        public IObservableCollection<Enemy> Enemies { get; }
    }

    public enum EnemyType
    {
        Birch,
        Hut
    }
}


