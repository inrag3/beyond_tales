using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Core.Health;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.GameObject
{
    public class CreatureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Creature>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Health>().AsSingle().NonLazy();
        }
    }
}