using System;
using System.Collections.Generic;
using _Project.Runtime.Config;
using _Project.Runtime.Core.Grenades.Ingredients;
using _Project.Runtime.Core.PauseHandler;
using _Project.Runtime.Infrastructure;
using _Project.Runtime.Infrastructure.Factories;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Mathf;
using Zenject;

namespace _Project.Runtime.Core.Enemies
{
    public class Enemy : Creature, IPauseHandler
    {
        [SerializeField] private Collider _collider;
        [field: SerializeField] public Point Point { get; private set; }

        protected Movement _movement;
        protected IAttacker _attack;

        protected EnemyAnimer _animer;
        private PauseHandlersRegister _pauseHandlersRegister;
        public event Action<Enemy> Died;

        protected IHerbalistProvider _provider;
        private List<string> dropPaths = new();
        private IInstantiator _instantiator;
        private IAssetManager _assetManager;
        private ILootConfig _lootConfig;

        [Inject]
        private void Construct(
            IHerbalistProvider provider,
            EnemyAnimer animer,
            Movement movement,
            Attack attack, PauseHandlersRegister pauseHandlersRegister, IInstantiator instantiator,
            IAssetManager assetManager, ILootConfig lootConfig)
        {
            _provider = provider;
            _animer = animer;
            _movement = movement;
            _attack = attack;
            _pauseHandlersRegister = pauseHandlersRegister;
            _pauseHandlersRegister.RegisterPauseHandler(this);
            _instantiator = instantiator;
            _assetManager = assetManager;
            _lootConfig = lootConfig;
            dropPaths.Add(lootConfig.LootRedPath);
            dropPaths.Add(lootConfig.LootBluePath);
            dropPaths.Add(lootConfig.LootGreenPath);
        }

        public bool CloseEnoughToAttack =>
            Vector3.SqrMagnitude(transform.position - _provider.Herbalist.Transform.position) <=
            Pow(_attack.Distance, 2f);

        public bool InAttackCooldown => _attack.InCooldown;

        private void Start()
        {
            _animer.Hitted += OnHitted;
            //Died += SpawnLoot;
        }

        private void OnDestroy()
        {
            _animer.Hitted -= OnHitted;
            //Died -= SpawnLoot;
        }

        public bool CanAttack() =>
            CloseEnoughToAttack && !InAttackCooldown && _provider.Herbalist.Health.Value.CurrentValue > 0 && !_animer.isPlayingHit();

        public bool CanRun() => !InAttackCooldown && !CloseEnoughToAttack &&
                                _provider.Herbalist.Health.Value.CurrentValue > 0 && !_animer.isPlayingHit();

        private void OnHitted()
        {
            _movement.Resume();
        }

        public override void TakeDamage(float value)
        {
            if (Health.Value.CurrentValue - value <= 0)
                SpawnLoot(this);
            _animer.PlayHit();
            _movement.Pause();
            base.TakeDamage(value);
        }

        protected override void Die()
        {
            _collider.enabled = false;
            _animer.PlayDeath();
            transform.DOScale(Vector3.zero, 0.15f);
            Died?.Invoke(this);
        }

        public void Move()
        {
            _movement.Move(_provider.Herbalist.Transform.position);
        }

        public void Attack()
        {
            _attack.Execute(_provider.Herbalist);
        }

        public void Destroy()
        {
        }

        public virtual void Pause()
        {
            _movement.Pause();
            _animer.Pause();
        }

        public virtual void Resume()
        {
            _movement.Resume();
            _animer.Resume();
        }

        private void SpawnLoot(Enemy enemy)
        {
            Vector3 spawnPosition = enemy.transform.position + Vector3.up * 3f; // Положение противника
            int lootCount = UnityEngine.Random.Range(1, _lootConfig.MaxLootCount);
            for (int i = 0; i < lootCount; i++)
            {
                GameObject prefab = _assetManager.Get(dropPaths[UnityEngine.Random.Range(0, dropPaths.Count)]);
                var ingredient = _instantiator.InstantiatePrefabForComponent<CollectableIngredient>(prefab);
                ingredient.transform.position = spawnPosition;
                ingredient.transform.parent = null;
            }
        }
    }
}