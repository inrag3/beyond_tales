using System.Collections;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Infrastructure.Installers.GameObject;
using _Project.Runtime.QuestSystem;
using ElectricServiceCompany;
using ObservableCollections;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class EnemySpawner : MonoBehaviour, ISpawner, IEnemiesProvider
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private int _enemySpawnCount = -1;
        [SerializeField] private BaseQuestAction[] _questActionsToActivateAfterAllEnemiesDied;

        private readonly ObservableHashSet<Enemy> _enemies = new();
        private IEnemyFactory _factory;
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;
        private ActorFactory _actorFactory;
        public IObservableCollection<Enemy> Enemies => _enemies;

        private int _spawnedEnemies = 0;
        private int _diedEnemies = 0;

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

        public void Stop()
        {
            if (!_coroutine.IsNullOrDestroyed())
            {
                StopCoroutine(_coroutine);
            }
        }

        public void Reset()
        {
            Stop();
            _spawnedEnemies = 0;
            _enemies.Clear();
            _diedEnemies = 0;
        }

        private IEnumerator Spawn()
        {
            while (_enemySpawnCount == -1 || _spawnedEnemies < _enemySpawnCount)
            { 
                Enemy enemy = _factory.Create(transform.position);
                _actorFactory.Create(enemy);
                enemy.Died += OnDied; 
                _enemies.Add(enemy);
                _spawnedEnemies++;
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

            _diedEnemies++;

            if (_enemySpawnCount != -1 && _diedEnemies == _enemySpawnCount)
            {
                foreach (var quest in _questActionsToActivateAfterAllEnemiesDied)
                {
                    quest.Activate();
                }
            }
        }
    }
}