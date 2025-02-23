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
        [SerializeField] private Scanner<Interactable> _scanner;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(transform).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Scanner<Interactable>>().FromInstance(_scanner).AsSingle().NonLazy();
            Container.Bind<Planter>().AsSingle();
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
            Container.Bind<HerbalistAnimer>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<Attacker>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Mover>().AsSingle();
            Container.BindInterfacesTo<Processor>().AsSingle().NonLazy();
            Container.BindInterfacesTo<Interactor>().AsSingle().NonLazy();
            Container.Bind<ItemProcessor>().AsSingle();
            Container.Bind<BedProcessor>().AsSingle();
            Container.Bind<DoorProcessor>().AsSingle();
            Container.Bind<GlobalWorldChangeProcessor>().AsSingle();
            Container.BindInterfacesAndSelfTo<Detector>().AsSingle().NonLazy();
        }
    }
}
