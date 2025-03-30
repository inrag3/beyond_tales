using System;
using UnityEngine;

namespace _Project.Runtime.QuestSystem
{
    public class StartDialogueQuestAction : BaseQuestAction
    {
        [SerializeField] private DialogueGraph _graph;
        [SerializeField] private SceneDialogueGraph _sceneGraph;

        public event Action<DialogueGraph> StartAction;
        
        public override void Activate()
        {
            if (_sceneGraph != null)
            {
                StartAction?.Invoke(_sceneGraph.graph);
            }
            else
            {
                StartAction?.Invoke(_graph);
            }
        }
    }
}