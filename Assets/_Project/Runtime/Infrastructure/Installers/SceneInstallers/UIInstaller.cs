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
            Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle().WithArguments(_healthView).NonLazy();
        }
    }
}