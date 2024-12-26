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
        
        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(_canvas).AsSingle().NonLazy();
            
            Container.BindInterfacesTo<IndicatorHandler>().AsSingle();
            Container.BindInterfacesTo<HintPresenter>().AsSingle().NonLazy();

            Container.BindInterfacesTo<HealthViewFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealthPresenterFactory>().AsSingle();
            
            Container.BindInterfacesTo<EnemiesHealthPresenter>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle().WithArguments(_healthView).NonLazy();
        }
    }
}