using _Project.Runtime.Config;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Infrastructure;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Grenades.PotionLogic
{
    public class PotionHeal : PotionBaseLogicProvider
    {
        private const string GRENADE_PATH = "Granade";
        private const string EXPOSION_PATH = "HealthPotionCenter";
        protected override string GrenadePath => GRENADE_PATH;
        protected override string ExplosionPath => EXPOSION_PATH;

        private readonly IHealth _health;
        private readonly IHerbalistProvider _herbalistProvider;

        public PotionHeal(
            IGrenadeConfig grenadeConfig,
            IAssetManager assetManager,
            IInstantiator instantiator,
            IHealth health,
            Vector3 mouse,
            Vector3 throwerPosition,
            IHerbalistProvider herbalistProvider
        ) : base(grenadeConfig, assetManager, instantiator, mouse, throwerPosition)
        {
            _health = health;
            _herbalistProvider = herbalistProvider;
        }

        public override void MakeAction()
        {
            _health.Increase(_grenadeConfig.PotionHealPoints);
            GameObject prefab = _assetManager.Get(ExplosionPath);
            var explosion = _instantiator.InstantiatePrefabForComponent<GrenadeExplosion>(prefab);
            explosion.transform.parent = _herbalistProvider.Herbalist.Transform;
            explosion.transform.localPosition = Vector3.zero;
            explosion.transform.rotation = Quaternion.identity;
            explosion.Timer.TimeEnded += () =>
            {
                explosion.SelfDestroy();
            };

            explosion.Timer.Start(_grenadeConfig.GrenadeExplosionTimeout);
            
        }
    }
}