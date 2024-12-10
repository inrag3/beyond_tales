using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class SceneInstaller : MonoInstaller
    {
        
        public override void InstallBindings()
        {
            Container.Bind<Bed[]>().FromMethod(_ => FindObjectsOfType<Bed>()).AsSingle();
        }
    }
}