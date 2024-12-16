using _Project.Runtime.AI.Core;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Infrastructure.Factories;

namespace _Project.Runtime.AI.Implementation
{
    public class Attack : IRule
    {
        public bool IsExecutable => _enemy.CloseEnoughToAttack
                                    && !_enemy.InAttackCooldown && _provider.Herbalist.Health.Value.CurrentValue > 0;

        private readonly Enemy _enemy;
        private readonly IHerbalistProvider _provider;
        
        public Attack(Enemy character, IHerbalistProvider provider)
        {
            _provider = provider;
            _enemy = character;
        }

        public void Execute() => 
            _enemy.Attack();
    }
}