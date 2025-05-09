using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;

public class BushGetWorldChange: Interactable
{
    private void Start()
    {
        IsAccessible = true;
    }

    public override void Interact(IInteractableVisitor visitor)
    {
        visitor.Accept(this);
    }
}