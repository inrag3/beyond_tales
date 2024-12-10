using UnityEngine;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.InventorySystem;

public class Flower : Item
{
    public override void Interact(IInteractableVisitor visitor)
    {
        if (!IsInteractable)
            return;
        
        IsInteractable = false;
        visitor.Accept(this);
        Destroy(gameObject);
    }

    public void Plant()
    {
        IsInteractable = false;
    }
}
