using System.Collections.Generic;
using System.Linq;
using _Project.Runtime.Core;
using _Project.Runtime.Infrastructure.Factories;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class SceneInstaller : MonoInstaller, IInitializable
    {
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneInstaller>().FromInstance(this).AsSingle().NonLazy();
            
            Container.Bind<Bed[]>().FromMethod(_ => FindObjectsOfType<Bed>()).AsSingle();
            
            Container.Bind<List<ISpawner>>().FromMethod(_ =>
            {
                var spawners = FindObjectsOfType<EnemySpawner>();
                return spawners.Select(spawner => spawner as ISpawner).ToList();
            }).AsSingle();
            
            Container.BindInterfacesAndSelfTo<Waver>().AsSingle().NonLazy();
        }
        
        public void Initialize()
        {
            Container.Resolve<HerbalistFactory>().Create();
        }
    }
}

