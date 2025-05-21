using _Project.Runtime.Core;
using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class ShowVictoryQuestAction : BaseQuestAction
    {
        [SerializeField] private VictoryHandler _victoryHandler;
        public override void Activate()
        {
            _victoryHandler.ShowVictory();
        }
    }
}