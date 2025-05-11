using System.Collections;
using _Project.Runtime.Core;
using _Project.Runtime.Core.Grenades;
using _Project.Runtime.Core.Grenades.GlobalWorldChange;
using _Project.Runtime.Core.Grenades.PotionLogic;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.PauseHandler;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.Infrastructure.Factories.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using _Project.Runtime.Audio;

namespace _Project.Runtime.Infrastructure.Installers
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CoroutinePerformer _coroutinePerformer;
        [SerializeField] private GizmosDrawer _gizmosDrawer;
        [SerializeField] private SoundSettings _soundSettings;
        [SerializeField] private AudioService _audioService;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneManager>().AsSingle().NonLazy();
            Container.Bind<GizmosDrawer>().FromInstance(_gizmosDrawer).AsSingle().NonLazy();
            Container.Bind<PauseHandlersRegister>().FromInstance(new PauseHandlersRegister()).AsSingle().NonLazy();
            
           
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

            Container.Bind<SearchSystem.SearchSystem>().AsSingle().NonLazy();

            Container.Bind<SoundSettings>().FromInstance(_soundSettings).AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<AudioService>().FromMethod((InjectContext context) => Instantiate(_audioService).GetComponent<AudioService>()).AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PotionsExplosionProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PoisonProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GlobalWorldChangeProvider>().AsSingle().NonLazy();

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
            Container.BindInterfacesAndSelfTo<PotionApplierFactory>().AsSingle().NonLazy();
        }
    }
}

public interface ICoroutinePerformer
{
    public Coroutine StartPerform(IEnumerator coroutine);

    public void StopPerform(Coroutine coroutine);
}