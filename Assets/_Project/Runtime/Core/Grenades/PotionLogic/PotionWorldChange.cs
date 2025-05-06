using System;
using _Project.Runtime.Config;
using _Project.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Grenades.PotionLogic
{
    public class PotionWorldChange : PotionBaseLogicProvider
    {
        private const string GRENADE_PATH = "Granade";
        private const string EXPOSION_PATH = "ExplosionCenter";
        protected override string GrenadePath => GRENADE_PATH;
        protected override string ExplosionPath => EXPOSION_PATH;
        
        private readonly Action<GrenadeExplosion> _removeExplosionFromList;
        private readonly Action<GrenadeExplosion> _addExplosionToList;

        public PotionWorldChange(
            IGrenadeConfig grenadeConfig,
            IAssetManager assetManager,
            IInstantiator instantiator,
            Vector3 mouse,
            Vector3 throwerPosition,
            Action<GrenadeExplosion> removeExplosionFromList,
            Action<GrenadeExplosion> addExplosionToLis
        ) : base(grenadeConfig, assetManager, instantiator, mouse, throwerPosition)
        {
            _removeExplosionFromList = removeExplosionFromList;
            _addExplosionToList = addExplosionToLis;
        }

        


        protected override void OnExplosionFinished(Grenade grenade, GrenadeExplosion explosion)
        {
            UpdateSecondWorldOverlap(explosion.transform.position, (a) => a.TryTriggerWorldChangeBack());

            _removeExplosionFromList.Invoke(explosion);
        }

        protected override void OnExplosionStart(Grenade grenade, GrenadeExplosion explosion)
        {
            UpdateSecondWorldOverlap(explosion.transform.position, (a) => a.TryTriggerWorldChange());
            explosion.Radius = _grenadeConfig.GrenadeTransformWorldRadius;
            _addExplosionToList.Invoke(explosion);
        }

        private void UpdateSecondWorldOverlap(Vector3 position, Action<SecondWorldExChangingTrigger> callback)
        {
            Collider[] hitColliders =
                Physics.OverlapSphere(position, _grenadeConfig.GrenadeTransformWorldRadius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out SecondWorldExChangingTrigger trigger))
                {
                    callback.Invoke(trigger);
                }
            }
        }
    }
}