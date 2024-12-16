using _Project.Runtime.AI.Core;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Infrastructure.Factories;

namespace _Project.Runtime.AI.Implementation
{
    public class Mover : IRule
    {
        private readonly Enemy _enemy;
        private readonly IHerbalistProvider _provider;

        public bool IsExecutable => !_enemy.CloseEnoughToAttack && _provider.Herbalist.Health.Value.CurrentValue > 0;

        public Mover(Enemy enemy, IHerbalistProvider provider)
        {
            _provider = provider;
            _enemy = enemy;
        }

        public void Execute() => 
            _enemy.Move();
    }
}