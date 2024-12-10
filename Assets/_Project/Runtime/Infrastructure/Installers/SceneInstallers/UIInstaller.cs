using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Infrastructure.Factories.UI;
using _Project.Runtime.Meta;
using _Project.Runtime.Meta.Health;
using _Project.Runtime.Meta.Interactables;
using UnityEngine;
using Zenject;
using HealthPresenter = _Project.Runtime.Meta.Health.HealthPresenter;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private HealthView _healthView;
        [SerializeField] private Canvas _canvas;
        
        private IAssetManager _assetManager;

        [Inject]
        private void Construct(IAssetManager assetManager)
        {
            _assetManager = assetManager;
        }
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<IndicatorHandler>().AsSingle();
            
            Container.BindInterfacesTo<HintPresenter>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<HealthViewFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemiesHealthPresenter>().AsSingle().NonLazy();
            
            Container.Bind<Canvas>().FromInstance(_canvas).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle().WithArguments(_healthView).NonLazy();
        }
    }
}