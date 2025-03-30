using System;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;

namespace _Project.Runtime.Core.Interactables
{
    public class DialogueInteractionTrigger : Interactable
    {
        [SerializeField] private DialogueGraph _graph;
        [SerializeField] private SceneDialogueGraph _sceneGraph;
        public event Action<DialogueGraph> Interacted;

        public override void Interact(IInteractableVisitor visitor)
        {
            Interact();
        }

        public void Interact()
        {
            if (!IsAccessible)
                return;

            if (_sceneGraph != null)
            {
                Interacted?.Invoke(_sceneGraph.graph);
            }
            else
            {
                Interacted?.Invoke(_graph);
            }
        }
    }
}