using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using _Project.Runtime.Config;
using _Project.Runtime.Infrastructure;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.InventorySystem;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class GrenadeThrower : IInitializable, ITickable, IDisposable, IGrenadeProvider

    {
        private const string GrenadePath = "Granade";
        private const string ExplosionPath = "ExplosionCenter";

        private bool _isRecoveringGrenades = false;
        private bool _readyToThrow = true;


        private readonly IHerbalistProvider _herbalistProvider;
        private readonly IInputService _inputService;

        private readonly IAssetManager _assetManager;
        private readonly IInstantiator _instantiator;
        private readonly IGrenadeConfig _grenadeConfig;
        private readonly Timer _throwCollDownTimer;
        private readonly Timer _grenadeRecoveryTimer;
        private readonly List<GrenadeExplosion> _explosions = new();
        private readonly IPlayerInventory _inventory;
        private readonly IItemContainer _itemContainer;
        public event Action<IReadOnlyList<GrenadeExplosion>> GrenadesUpdated;

        public GrenadeThrower(
            IHerbalistProvider herbalistProvider,
            IInputService inputService,
            IInstantiator instantiator,
            IAssetManager assetManager,
            IGrenadeConfig grenadeConfig,
            IPlayerInventory inventory,
            IItemContainer itemContainer,
            Timer throwCollDownTimer,
            Timer grenadeRecoveryTimer
        )
        {
            _herbalistProvider = herbalistProvider;
            _inputService = inputService;
            _instantiator = instantiator;
            _assetManager = assetManager;
            _grenadeConfig = grenadeConfig;
            _inventory = inventory;
            _itemContainer = itemContainer;
            _throwCollDownTimer = throwCollDownTimer;
            _grenadeRecoveryTimer = grenadeRecoveryTimer;
        }

        public void Initialize()
        {
            _throwCollDownTimer.TimeEnded += ResetThrow;
            _grenadeRecoveryTimer.TimeEnded += RecoverGrenade;
        }

        public void Tick()
        {
            if (!(_inputService.IsGrenadeButtonPressed && _readyToThrow && _inventory.Items[ItemEnum.Grenade] > 0))
                return;
            _readyToThrow = false;
            _inventory.RemoveItem(ItemEnum.Grenade, 1);

            GameObject prefab = _assetManager.Get(GrenadePath);
            var grenade = _instantiator.InstantiatePrefabForComponent<Grenade>(prefab);
            grenade.transform.position = _herbalistProvider.Herbalist.Transform.position;
            grenade.transform.position += Vector3.up;
            grenade.transform.parent = null;

            Rigidbody rigidbody = grenade.GetComponent<Rigidbody>();

            Vector3 forceToAdd = _herbalistProvider.Herbalist.Transform.forward * _grenadeConfig.GrenadeFrontForce +
                                 prefab.transform.up * _grenadeConfig.GrenadeUpForce;
            rigidbody.AddForce(forceToAdd, ForceMode.Impulse);

            grenade.Hit += GrenadeContactCallback;

            _throwCollDownTimer.Start(_grenadeConfig.GrenadeThrowsTimeout);


            if (!_isRecoveringGrenades)
            {
                _isRecoveringGrenades = true;
                _grenadeRecoveryTimer.Start(_grenadeConfig.GrenadeRecoveryTimeout);
            }
        }

        private void GrenadeContactCallback(Grenade grenade)
        {
            GameObject prefab = _assetManager.Get(ExplosionPath);
            var explosion = _instantiator.InstantiatePrefabForComponent<GrenadeExplosion>(prefab);
            explosion.transform.parent = grenade.transform.parent;
            explosion.transform.position = grenade.transform.position;

            explosion.Timer.TimeEnded += () =>
            {
                _explosions.Remove(explosion);
                GrenadesUpdated?.Invoke(new ReadOnlyCollection<GrenadeExplosion>(_explosions));
                UpdateSecondWorldOverlap(explosion.transform.position, (a) => a.TriggerWorldChangeBack());
                explosion.SelfDestroy();
            };

            explosion.Timer.Start(_grenadeConfig.GrenadeExplosionTimeout);

            _explosions.Add(explosion);

            UpdateSecondWorldOverlap(grenade.transform.position, (a) => a.TriggerWorldChange());

            GrenadesUpdated?.Invoke(new ReadOnlyCollection<GrenadeExplosion>(_explosions));
        }

        private void UpdateSecondWorldOverlap(Vector3 position, Action<SecondWorldExChangingTrigger> callback)
        {
            Collider[] hitColliders =
                Physics.OverlapSphere(position, _grenadeConfig.GrenadeTransformWorldRadius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent<SecondWorldExChangingTrigger>(out SecondWorldExChangingTrigger trigger))
                {
                    callback.Invoke(trigger);
                }
            }
        }

        private void ResetThrow()
        {
            _readyToThrow = true;
        }

        private void RecoverGrenade()
        {
            _inventory.AddItem(ItemEnum.Grenade, 1);
            if (_inventory.Items[ItemEnum.Grenade] < _itemContainer.ItemData[ItemEnum.Grenade].MaxStackSize)
            {
                _grenadeRecoveryTimer.Start(_grenadeConfig.GrenadeRecoveryTimeout);
            }
            else
            {
                _isRecoveringGrenades = false;
            }
        }

        public void Dispose()
        {
            _throwCollDownTimer.TimeEnded -= ResetThrow;
            _grenadeRecoveryTimer.TimeEnded -= RecoverGrenade;
        }
    }

    public interface IGrenadeProvider
    {
        public event Action<IReadOnlyList<GrenadeExplosion>> GrenadesUpdated;
    }
}