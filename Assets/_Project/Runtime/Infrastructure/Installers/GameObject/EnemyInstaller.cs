using _Project.Runtime.Core.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Runtime.Infrastructure.Installers.GameObject
{
    public sealed class EnemyInstaller : CreatureInstaller
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.Bind<Enemy>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<NavMeshAgent>().FromInstance(_agent).AsSingle().NonLazy();
            Container.Bind<Animator>().FromInstance(_animator).AsSingle().NonLazy();
            
            Container.Bind<Movement>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<Attack>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<EnemyAnimer>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}