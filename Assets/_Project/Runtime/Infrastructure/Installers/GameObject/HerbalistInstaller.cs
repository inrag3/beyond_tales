using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.GameObject
{
    public sealed class HerbalistInstaller : MonoInstaller
    {
        [SerializeField] private Animator _animator;

        public override void InstallBindings()
        {
            Container.Bind<Planter>().AsSingle();
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
            Container.Bind<Animer>().AsSingle();
            Container.BindInterfacesAndSelfTo<Mover>().AsSingle();
            Container.BindInterfacesAndSelfTo<Scanner<Interactable>>().AsSingle().NonLazy();
            Container.BindInterfacesTo<Processor>().AsSingle().NonLazy();
            Container.BindInterfacesTo<Interactor>().AsSingle().NonLazy();
            Container.Bind<ItemProcessor>().AsSingle();
            Container.Bind<BedProcessor>().AsSingle();
        }
    }
}
