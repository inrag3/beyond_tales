using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class ActivateObjectQuestAction : BaseQuestAction
    {
        [SerializeField] private GameObject _object;
        [SerializeField] private bool _makeActive;
        
        public override void Activate()
        {
            _object.SetActive(_makeActive);
        }
    }
}