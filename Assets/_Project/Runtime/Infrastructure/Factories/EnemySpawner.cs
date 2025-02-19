using System.Collections;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Infrastructure.Installers.GameObject;
using ObservableCollections;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class EnemySpawner : MonoBehaviour, ISpawner, IEnemiesProvider
    {
        [SerializeField] private float _cooldown;

        private readonly ObservableHashSet<Enemy> _enemies = new();
        private IEnemyFactory _factory;
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;
        private ActorFactory _actorFactory;
        public IObservableCollection<Enemy> Enemies => _enemies;

        [Inject]
        private void Construct(IEnemyFactory factory, ActorFactory actorFactory)
        {
            _actorFactory = actorFactory;
            _factory = factory;
        }
        
        public void Begin()
        {
            _coroutine = StartCoroutine(Spawn());
        }
        public void Stop() => 
            StopCoroutine(_coroutine);
        
        private IEnumerator Spawn()
        {
            while (true)
            { 
                Enemy enemy = _factory.Create(transform.position);
                _actorFactory.Create(enemy);
                enemy.Died += OnDied; 
                _enemies.Add(enemy);
                _waitForSeconds = new WaitForSeconds(_cooldown);
                yield return _waitForSeconds;
            }
        }
        private void OnDied(Enemy enemy)
        {
            //TODO сделать пул объектов, чтобы не спавнить по миллион раз
            enemy.Died -= OnDied;
            _enemies.Remove(enemy);
            enemy.Destroy();
        }
    }
}