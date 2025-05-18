using System;
using _Project.Runtime.Config;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Grenades.PotionLogic
{
    public class PoisonPotion: PotionBaseLogicProvider
    {
        private const string GRENADE_PATH = "PoisonPotion";
        private const string EXPOSION_PATH = "PoisonPotionCenter";
        protected override string GrenadePath => GRENADE_PATH;
        protected override string ExplosionPath => EXPOSION_PATH;
        protected readonly Action<Enemy> _onPoison;

        public PoisonPotion(
            IGrenadeConfig grenadeConfig,
            IAssetManager assetManager,
            IInstantiator instantiator,
            Vector3 mouse,
            Vector3 throwerPosition,
            Action<Enemy> onPoison
        ) : base(grenadeConfig, assetManager, instantiator, mouse, throwerPosition)
        {
            _onPoison = onPoison;
        }

        protected override void OnExplosionStart(Grenade grenade, GrenadeExplosion explosion)
        {
            explosion.transform.rotation = Quaternion.identity;
            ((PoisonPotionCenter)explosion).EnemyContacted += _onPoison;
        }
    }
}