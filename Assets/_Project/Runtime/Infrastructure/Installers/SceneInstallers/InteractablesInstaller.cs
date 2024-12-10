using _Project.Runtime.Core.Interactables.Processors; 
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class InteractablesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Processor>().AsSingle();
            Container.Bind<ItemProcessor>().AsSingle();
        }
    }
}