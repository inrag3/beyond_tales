using System.Collections.Generic;
using System.Linq;
using _Project.Runtime.AI.Core;
using _Project.Runtime.Core;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.Infrastructure.Installers.GameObject;
using _Project.Runtime.QuestSystem;
using _Project.Runtime.SearchSystem;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class SceneInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        private Vector3 herbalistStartPosition;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneInstaller>().FromInstance(this).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ActorFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ActorUpdater>().AsSingle().NonLazy();
            BindEnemySpawners();
            
            Container.Bind<Bed[]>().FromMethod(_ => FindObjectsOfType<Bed>()).AsSingle();
            Container.Bind<Door[]>().FromMethod(_ => FindObjectsOfType<Door>()).AsSingle();
            Container.BindInterfacesAndSelfTo<Waver>().AsSingle().NonLazy();
            
            BindSearchIndices();
        }

        public void Initialize()
        {
            Container.Resolve<HerbalistFactory>().Create(herbalistStartPosition);
        }

        private void BindEnemySpawners()
        {
            var spawners = FindObjectsOfType<EnemySpawner>();
            var list = spawners.Select(spawner => spawner as ISpawner).ToList();
            Container.Bind<List<ISpawner>>().FromInstance(list).AsSingle();
            var enemiesProviders = spawners.Select(spawner => spawner as IEnemiesProvider).ToList();
            Container.Bind<List<IEnemiesProvider>>().FromInstance(enemiesProviders).AsSingle();
        }

        private void BindQuestActions()
        {
            var storyMarks = FindObjectsOfType<AddStoryMarksQuestAction>();
            Container.Bind<AddStoryMarksQuestAction[]>().FromInstance(storyMarks).AsSingle();
        }

        private void BindSearchIndices()
        {
            var indices = FindObjectsOfType<SearchIndex>();
            Container.Bind<SearchIndex[]>().FromInstance(indices).AsSingle();
        }
    }

    public class ActorUpdater : ITickable
    {
        private readonly IActorRepository _actorRepository;

        public ActorUpdater(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public void Tick()
        {
            foreach (IActor actor in _actorRepository.Actors.ToArray())
            {
                actor.Tick();
            }
        }
    }
}

