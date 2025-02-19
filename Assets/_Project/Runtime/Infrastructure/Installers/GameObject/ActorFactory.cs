using System.Collections.Generic;
using _Project.Runtime.AI.Core;
using _Project.Runtime.AI.Implementation;
using _Project.Runtime.Core.Enemies;
using Zenject;
using Attack = _Project.Runtime.AI.Implementation.Attack;

namespace _Project.Runtime.Infrastructure.Installers.GameObject
{
    public class ActorFactory : IActorRepository
    {
        private readonly IInstantiator _instantiator;
        private readonly List<IActor> _actors = new();
        public ActorFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public IReadOnlyCollection<IActor> Actors => _actors;

        public void Create(Enemy enemy)
        {
            var mover = _instantiator.Instantiate<Mover>(new []{ enemy });
            var attack = _instantiator.Instantiate<Attack>(new []{ enemy });
            var actor = new Actor(mover, attack);
            _actors.Add(actor);
            enemy.Died += Dispose;
            return;
            
            void Dispose(Enemy _)
            {
                enemy.Died -= Dispose;
                _actors.Remove(actor);
            }
        }
    }

    public interface IActorRepository
    {
        public IReadOnlyCollection<IActor> Actors { get; } 
    }
}