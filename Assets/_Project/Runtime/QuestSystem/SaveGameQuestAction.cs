using _Project.Runtime.Audio;
using _Project.Runtime.Core.SaveSystem;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.QuestSystem
{
    public class SaveGameQuestAction : BaseQuestAction
    {
        [SerializeField] private Transform _checkPoint;

        private GameSaver _gameSaver;
        
        [Inject]
        private void Construct(GameSaver gameSaver)
        {
            _gameSaver = gameSaver;
        }
        public override void Activate()
        {
            _gameSaver.SaveGame(_checkPoint.position);
        }
    }
}