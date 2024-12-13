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

            BindEnemySpawners();
            
            Container.Bind<Bed[]>().FromMethod(_ => FindObjectsOfType<Bed>()).AsSingle();
            Container.BindInterfacesAndSelfTo<Waver>().AsSingle().NonLazy();
        }

        public void Initialize()
        {
            Container.Resolve<HerbalistFactory>().Create();
        }

        private void BindEnemySpawners()
        {
            var spawners = FindObjectsOfType<EnemySpawner>();
            var list = spawners.Select(spawner => spawner as ISpawner).ToList();
            Container.Bind<List<ISpawner>>().FromInstance(list).AsSingle();
            var enemiesProviders = spawners.Select(spawner => spawner as IEnemiesProvider).ToList();
            Container.Bind<List<IEnemiesProvider>>().FromInstance(enemiesProviders).AsSingle();
        }
    }
}

