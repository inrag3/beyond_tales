using _Project.Runtime.Core.Herbalist;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.GameObject
{
    public sealed class HerbalistInstaller : MonoInstaller
    {
        [SerializeField] private Animator _animator;

        public override void InstallBindings()
        {
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
            Container.Bind<Animer>().AsSingle();

            Container.BindInterfacesAndSelfTo<Mover>().AsSingle();
        }
    }
}
