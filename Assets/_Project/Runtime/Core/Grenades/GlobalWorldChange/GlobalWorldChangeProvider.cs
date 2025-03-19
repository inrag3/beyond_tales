using System;
using System.Collections.Generic;
using _Project.Runtime.Core.Grenades.PotionLogic;
using _Project.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Grenades.GlobalWorldChange
{
    public class GlobalWorldChangeProvider : IGlobalWorldChangeSubscriptable, IGlobalWorldChangeProvider
    {
        private readonly PotionsExplosionProvider _localPotionsExplosionProvider;
        private readonly IInstantiator _instantiator;
        private readonly IAssetManager _assetManager;
        private const string EXPOSION_PATH = "ExplosionCenter";

        private GrenadeExplosion _worldChangeExplosion = null;
        private List<SecondWorldExChangingTrigger> _changedObjects = new();

        public event Action<GrenadeExplosion> OnGlobalWorldChange;

        public GlobalWorldChangeProvider(PotionsExplosionProvider potionsExplosionProvider, IInstantiator instantiator,
            IAssetManager assetManager)
        {
            _localPotionsExplosionProvider = potionsExplosionProvider;
            _instantiator = instantiator;
            _assetManager = assetManager;
        }

        public bool IsAvailable => !_localPotionsExplosionProvider.IsAnyExplosionsActive;

        public bool IsActive => _worldChangeExplosion is not null;

        public bool Activate(Vector3 position, float radius)
        {
            if (!IsAvailable || IsActive)
                return false;
            GameObject prefab = _assetManager.Get(EXPOSION_PATH);
            var explosion = _instantiator.InstantiatePrefabForComponent<GrenadeExplosion>(prefab);
            explosion.Radius = radius;
            explosion.transform.position = position;
            _worldChangeExplosion = explosion;
            UpdateSecondWorldOverlap(position, radius);
            OnGlobalWorldChange?.Invoke(_worldChangeExplosion);
            
            return true;
        }

        public void Deactivate()
        {
            _changedObjects.ForEach(e => e.TriggerWorldChangeBack());
            _changedObjects.Clear();
            _worldChangeExplosion.SelfDestroy();
            _worldChangeExplosion = null;
            OnGlobalWorldChange?.Invoke(_worldChangeExplosion);
        }

        private void UpdateSecondWorldOverlap(Vector3 position, float radius)
        {
            Collider[] hitColliders =
                Physics.OverlapSphere(position, radius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out SecondWorldExChangingTrigger trigger))
                {
                    trigger.TriggerWorldChange();
                    _changedObjects.Add(trigger);
                }
            }
        }
    }

    public interface IGlobalWorldChangeSubscriptable
    {
        public event Action<GrenadeExplosion> OnGlobalWorldChange;
    }

    public interface IGlobalWorldChangeProvider
    {
        public bool IsAvailable { get; }
        public bool IsActive { get; }
        public bool Activate(Vector3 position, float radius);
        public void Deactivate();
    }
}