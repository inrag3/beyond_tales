using System;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Infrastructure.Factories;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers
{
    public sealed class ProjectInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ProjectInstaller>().FromInstance(this).AsSingle().NonLazy();


            BindAssetManager();
            BindFactories();
            BindServices();

            Container.BindInterfacesTo<Health>().AsTransient().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle().NonLazy();
        }

        private void BindAssetManager()
        {
            Container.BindInterfacesTo<AssetManager>().AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<HerbalistFactory>().AsSingle().NonLazy();
        }

        [Obsolete]
        public void Initialize()
        {
            Container.Resolve<HerbalistFactory>().Create();
        }
    }
}