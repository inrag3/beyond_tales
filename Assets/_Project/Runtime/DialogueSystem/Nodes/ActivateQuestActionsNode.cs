using _Project.Runtime.QuestSystem;
using UnityEngine;

namespace DialogueSystem.Nodes
{
    [NodeTint("#000000")]
    public class ActivateQuestActionsNode : DialogueBaseNode
    {
        [SerializeField] public BaseQuestAction[] QuestActions;
    }
}