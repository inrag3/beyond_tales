using _Project.Runtime.Config;
using _Project.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Grenades.PotionLogic
{
    public class ExplosionPotion : PotionBaseLogicProvider
    {
        private const string GRENADE_PATH = "ExplosionGrenade";
        private const string EXPOSION_PATH = "ExplotionPotionCenter";
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
            explosion.transform.rotation = Quaternion.identity;
        }

    }
}