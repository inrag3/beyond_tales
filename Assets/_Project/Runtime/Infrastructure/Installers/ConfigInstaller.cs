using _Project.Runtime.Config.Herbalist;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers
{
    public sealed class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private HerbalistConfig herbalistConfig;

        public override void InstallBindings()
        {
            BindHerbalistConfig();
        }

        private void BindHerbalistConfig()
        {
            Container.BindInterfacesTo<HerbalistConfig>().FromInstance(herbalistConfig).AsSingle();
        }
    }
}