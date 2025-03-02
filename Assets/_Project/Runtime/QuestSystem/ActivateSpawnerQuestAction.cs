using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class ActivateSpawnerQuestAction : BaseQuestAction
    {
        [SerializeField] private EnemySpawner[] _spawners;
        public override void Activate()
        {
            foreach (var spawner in _spawners)
            {
                spawner.Begin();
            }
        }
    }
}