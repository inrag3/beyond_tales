using _Project.Runtime.Infrastructure.Factories.UI;
using _Project.Runtime.Meta.Health;
using UnityEngine;
using Zenject;
using HealthPresenter = _Project.Runtime.Meta.Health.HealthPresenter;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private HealthView _healthView;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<IndicatorHandler>().AsSingle();
            
            Container.BindInterfacesTo<HealthViewFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemiesHealthPresenter>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle().WithArguments(_healthView).NonLazy();
        }
    }
}