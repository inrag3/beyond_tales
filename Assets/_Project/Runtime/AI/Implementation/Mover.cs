using _Project.Runtime.AI.Core;
using _Project.Runtime.Core.Enemies;

namespace _Project.Runtime.AI.Implementation
{
    public class Mover : IRule
    {
        private readonly Enemy _enemy;

        public bool IsExecutable => !_enemy.CloseEnoughToAttack;

        public Mover(Enemy enemy) => 
            _enemy = enemy;

        public void Execute() => 
            _enemy.Move();
    }
}