using _Project.Runtime.Core.Interactables.Items;
using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.InventorySystem;
using UnityEngine;

public class Flower : Item
{
    [SerializeField] private ItemEnum flowerType;
    public ItemEnum FlowerType => flowerType;

    public bool IsPlanted => transform.parent != null && transform.parent.GetComponent<Bed>() != null;

    public override void Interact(IInteractableVisitor visitor)
    {
        if (!IsAccessible) { return; }
        Bed bed = transform.parent != null ? transform.parent.GetComponent<Bed>() : null;
        if (bed != null)
        {
            if (!bed.IsCorrectFlowerPlanted)
            {
                bed.Unplant();
                visitor.Accept(this);
                Destroy(gameObject);
                return;
            }
            else { return; }
        }
        IsAccessible = false;
        visitor.Accept(this);
        Destroy(gameObject);
    }

    public void Plant()
    { 
        IsAccessible = false;
    }
}
