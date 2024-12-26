using System.Collections;
using _Project.Runtime.Core;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.PauseHandler;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.Infrastructure.Factories.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CoroutinePerformer _coroutinePerformer;
        [SerializeField] private GizmosDrawer _gizmosDrawer;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneManager>().AsSingle().NonLazy();
            Container.Bind<GizmosDrawer>().FromInstance(_gizmosDrawer).AsSingle().NonLazy();
            
           
            BindAssetManager();
            BindFactories();
            BindServices();

            Container.BindInterfacesTo<Health>().AsSingle().NonLazy();
            
            Container.Bind<Timer>().AsTransient().NonLazy();
            Container.BindInterfacesAndSelfTo<GrenadeThrower>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<ItemContainer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInventory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InventoryTester>().AsSingle().NonLazy();
            
            
            Container.BindInterfacesAndSelfTo<PauseHandler>().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle().NonLazy();
            
            Container.Bind<ICoroutinePerformer>().FromInstance(_coroutinePerformer).AsSingle().NonLazy();
        }

        private void BindAssetManager()
        {
            Container.BindInterfacesTo<AssetManager>().AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<HerbalistFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FlowerFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HintViewFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle().NonLazy();
        }
    }
}

public interface ICoroutinePerformer
{
    public Coroutine StartPerform(IEnumerator coroutine);

    public void StopPerform(Coroutine coroutine);
}