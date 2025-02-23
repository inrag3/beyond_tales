using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist.GlobalWorldChange
{
    public class GlobalWorldChangeTrigger : Interactable
    {
        [SerializeField] public float Radius;

        public override void Interact(IInteractableVisitor visitor)
        {
            if (!IsAccessible)
                return;

            visitor.Accept(this);
        }
    }
}