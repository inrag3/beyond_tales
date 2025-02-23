using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;

namespace _Project.Runtime.Core.Herbalist.GlobalWorldChange
{
    public class GlobalWorldChangeBackTrigger:Interactable
    {
        public override void Interact(IInteractableVisitor visitor)
        {
            if (!IsAccessible)
                return;

            visitor.Accept(this);
        }
    }
}