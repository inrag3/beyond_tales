using _Project.Runtime.Core.Interactables.Items;
using _Project.Runtime.Core.Interactables.Processors;

public class Flower : Item
{
    public override void Interact(IInteractableVisitor visitor)
    {
        if (!IsAccessible)
            return;
        
        IsAccessible = false;
        visitor.Accept(this);
        Destroy(gameObject);
    }

    public void Plant()
    {
        IsAccessible = false;
    }
}
