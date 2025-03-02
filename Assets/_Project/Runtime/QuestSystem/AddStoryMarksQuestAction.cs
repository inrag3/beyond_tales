using System.Collections.Generic;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.QuestSystem
{
    public class AddStoryMarksQuestAction : BaseQuestAction
    {
        [SerializeField] private string[] _storyMarks;
        
        private IHerbalistProvider _herbalistProvider;
        
        [Inject]
        public void Construct(IHerbalistProvider herbalistProvider)
        {
            _herbalistProvider = herbalistProvider;
        }
        public override void Activate()
        {
            _herbalistProvider.Herbalist.PlayerData.AddStoryMarks(_storyMarks);
        }
    }
}