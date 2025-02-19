using System;
using _Project.Runtime.Config;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class PotionHeal : PotionBaseLogicProvider
    {
        private const string GRENADE_PATH = "Granade";
        private const string EXPOSION_PATH = "ExplosionCenter";
        protected override string GrenadePath => GRENADE_PATH;
        protected override string ExplosionPath => EXPOSION_PATH;

        private readonly IHealth _health;

        public PotionHeal(
            IGrenadeConfig grenadeConfig,
            IAssetManager assetManager,
            IInstantiator instantiator,
            IHealth health,
            Vector3 mouse,
            Vector3 throwerPosition
        ) : base(grenadeConfig, assetManager, instantiator, mouse, throwerPosition)
        {
            _health = health;
        }

        public override void MakeAction()
        {
            _health.Increase(_grenadeConfig.PotionHealPoints);
        }
    }
}