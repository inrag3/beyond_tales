using System;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Core.Health;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.GameObject
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        
        public override void InstallBindings()
        {
            Container.Bind<Enemy>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<NavMeshAgent>().FromInstance(_agent).AsSingle().NonLazy();
            Container.Bind<Animator>().FromInstance(_animator).AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<Health>().AsSingle().NonLazy();
            
            Container.Bind<Movement>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<Attack>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<EnemyAnimer>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}