using _Project.Runtime.Core.Interactables.Processors;

namespace _Project.Runtime.Core.Interactables
{
    public class Door : Interactable
    {
        public override void Interact(IInteractableVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}