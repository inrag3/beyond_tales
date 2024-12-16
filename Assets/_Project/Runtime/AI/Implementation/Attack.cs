using _Project.Runtime.AI.Core;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Infrastructure.Factories;

namespace _Project.Runtime.AI.Implementation
{
    public class Attack : IRule
    {
        public bool IsExecutable => _enemy.CloseEnoughToAttack
                                    && !_enemy.InAttackCooldown;

        private readonly Enemy _enemy;
        private IHerbalistProvider _provider;

        public Attack(Enemy character)
        {
            _enemy = character;
        }

        public void Execute() => 
            _enemy.Attack();
    }
}