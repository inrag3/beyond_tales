using _Project.Runtime.Audio;
using _Project.Runtime.Core.SaveSystem;
using _Project.Runtime.Core.UI;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.QuestSystem
{
    public class SaveGameQuestAction : BaseQuestAction
    {
        [SerializeField] private Transform _checkPoint;

        private GameSaver _gameSaver;
        private SaveGameNotifier _saveGameNotifier;
        
        [Inject]
        private void Construct(GameSaver gameSaver, SaveGameNotifier saveGameNotifier)
        {
            _gameSaver = gameSaver;
            _saveGameNotifier = saveGameNotifier;
        }
        public override void Activate()
        {
            _gameSaver.SaveGame(_checkPoint.position);
            _saveGameNotifier.SaveGameNotify();
        }
    }
}