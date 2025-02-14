using _Project.Runtime.Config;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class ExplosionPotion : PotionBaseLogicProvider
    {
        private const string GRENADE_PATH = "Granade";
        private const string EXPOSION_PATH = "ExplosionCenter";
        protected override string GrenadePath => GRENADE_PATH;
        protected override string ExplosionPath => EXPOSION_PATH;

        public ExplosionPotion(
            IGrenadeConfig grenadeConfig,
            IAssetManager assetManager,
            IInstantiator instantiator,
            Vector3 mouse,
            Vector3 throwerPosition
        ) : base(grenadeConfig, assetManager, instantiator, mouse, throwerPosition)
        {
        }

        protected override void OnExplosionStart(Grenade grenade, GrenadeExplosion explosion)
        {
            DamageEnemies(grenade);
        }

        private void DamageEnemies(Grenade grenade)
        {
            Collider[] hitColliders =
                Physics.OverlapSphere(grenade.transform.position, _grenadeConfig.PotionExplosionRadius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_grenadeConfig.PotionExplosionDamage);
                }
            }
        }
    }
}