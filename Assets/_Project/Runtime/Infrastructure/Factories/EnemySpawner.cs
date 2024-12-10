using System.Collections;
using ObservableCollections;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class EnemySpawner : MonoBehaviour, ISpawner, IEnemiesProvider
    {
        [SerializeField] private float _cooldown;
        
        private IEnemyFactory _factory;
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;
        private ObservableHashSet<Enemy> _enemies;
        public IObservableCollection<Enemy> Enemies => _enemies;

        [Inject]
        private void Construct(IEnemyFactory factory)
        {
            _factory = factory;
        }
        
        public void Begin()
        {
            _coroutine = StartCoroutine(Spawn());
        }
        
        private IEnumerator Spawn()
        {
            while (true)
            { 
                Enemy enemy = _factory.Create(transform.position);
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

        public void Pause() => 
            StopCoroutine(_coroutine);

        public void Unpause() => 
            Begin();
    }
}