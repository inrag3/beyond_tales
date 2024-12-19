using _Project.Runtime.Core.Interactables.Items;
using _Project.Runtime.Core.Interactables.Processors;
using System;

public class Flower : Item
{
    public event Action<Flower> Interacted;
    public override void Interact(IInteractableVisitor visitor)
    {
        if (!IsAccessible)
            return;
        
        IsAccessible = false;
        visitor.Accept(this);
        Interacted?.Invoke(this);
        Destroy(gameObject);
    }

    public void Plant()
    {
        IsAccessible = false;
    }
}
