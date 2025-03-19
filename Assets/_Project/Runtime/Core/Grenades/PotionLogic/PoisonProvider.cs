using System.Collections.Generic;
using _Project.Runtime.Config;
using _Project.Runtime.Core.Enemies;
using UnityEngine;

namespace _Project.Runtime.Core.Grenades.PotionLogic
{
    public class PoisonProvider: IPoisoningProvider
    {
        private readonly IGrenadeConfig _grenadeConfig;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private List<PoisonWatcher> _poisonWatchers = new();

        public PoisonProvider(
            IGrenadeConfig grenadeConfig,
            ICoroutinePerformer coroutinePerformer
        )
        {
            _grenadeConfig = grenadeConfig;
            _coroutinePerformer = coroutinePerformer;
        }

        public void StartPoisoning(Enemy enemy)
        {
            var poisonWatcher = new PoisonWatcher(_coroutinePerformer, enemy, _grenadeConfig.PotionPoisonTimesCount);
            poisonWatcher.Timer.TimeEnded += () =>
            {
                Debug.Log("timer");
                if (poisonWatcher.TargetEnemy is not null && poisonWatcher.TargetEnemy.Health.Value.CurrentValue > 0)
                {
                    poisonWatcher.TargetEnemy.TakeDamage(_grenadeConfig.PotionPoisonDamage);
                    poisonWatcher.PoisoningTimeLeast--;


                    if (poisonWatcher.PoisoningTimeLeast > 0)
                    {
                        poisonWatcher.Timer.Start(_grenadeConfig.PotionPoisonTimeDelay);
                        return;
                    }
                }
                _poisonWatchers.Remove(poisonWatcher);
            };
            _poisonWatchers.Add(poisonWatcher);
            poisonWatcher.Timer.Start(_grenadeConfig.PotionPoisonTimeDelay);
        }


        private class PoisonWatcher
        {
            public readonly Timer Timer;
            public readonly Enemy TargetEnemy;
            public int PoisoningTimeLeast;

            public PoisonWatcher(ICoroutinePerformer coroutinePerformer,
                Enemy enemy, int poisoningTime
            )
            {
                Timer = new Timer(coroutinePerformer);
                TargetEnemy = enemy;
                PoisoningTimeLeast = poisoningTime;
            }
        }
    }

    public interface IPoisoningProvider
    {
        void StartPoisoning(Enemy enemy);
    }
    
}