using _Project.Runtime.Core.Interactables.Items;
using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.QuestSystem;
using UnityEngine;
using _Project.Runtime.InventorySystem;
using UnityEngine;

public class Flower : Item
{
    [SerializeField] private BaseQuestAction[] _questActionsWhenGet;
    public ItemEnum FlowerType => Data.ItemEnum;

    public bool IsPlanted => transform.parent != null && transform.parent.GetComponent<Bed>() != null;
    

    public override void Interact(IInteractableVisitor visitor)
    {
        if (!IsAccessible) { return; }
        Bed bed = transform.parent != null ? transform.parent.GetComponent<Bed>() : null;
        if (bed != null)
        {
            /*if (!bed.IsCorrectFlowerPlanted)
            {
                bed.Unplant();
                visitor.Accept(this);
                Destroy(gameObject);
                return;
            }
            else { return; }*/
            IsAccessible = false;
            return;
        }
        IsAccessible = false;
        visitor.Accept(this);

        if (_questActionsWhenGet != null)
        {
            foreach (var action in _questActionsWhenGet)
            {
                action.Activate();
            }
        }

        Destroy(gameObject);
    }

    public void Plant()
    {
        IsAccessible = false;
    }
}
