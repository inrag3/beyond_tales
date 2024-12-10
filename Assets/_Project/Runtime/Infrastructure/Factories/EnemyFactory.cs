using System;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.PauseHandler;
using ObservableCollections;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory, IInitializable
    {
        private const string EnemyPath = "Enemy";

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
            return _instantiator.InstantiatePrefabForComponent<Enemy>(_prefab);
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

    public class Enemy : MonoBehaviour,  IDamageable, ITransformable, IPauseHandler
    {
        private IHerbalistProvider _herbalistProvider;
        private IHealth _health;

        [Inject]
        private void Construct(IHerbalistProvider herbalistProvider, IHealth health)
        {
            _health = health;
            _herbalistProvider = herbalistProvider;
        }
        public IHealth Health { get; private set; }
        public Transform Transform => transform;
        public event Action<Enemy> Died;

        public void TakeDamage(int value)
        {
            Health.Decrease(value);
            if (_health.Value.CurrentValue == 0)
            {
                Died?.Invoke(this);
            }
        }
        
        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        public void Unpause()
        {
            throw new System.NotImplementedException();
        }

        public void Destroy()
        {
            
        }
    }

    public interface IEnemiesProvider
    {
        public IObservableCollection<Enemy> Enemies { get; }
    }
}


