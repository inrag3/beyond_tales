using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;

namespace _Project.Runtime.Core.Grenades.GlobalWorldChange
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