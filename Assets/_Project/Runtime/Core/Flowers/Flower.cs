using _Project.Runtime.Core.Interactables.Items;
using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.QuestSystem;
using UnityEngine;

public class Flower : Item
{
    [SerializeField] private BaseQuestAction[] _questActionsWhenGet;
    public override void Interact(IInteractableVisitor visitor)
    {
        if (!IsAccessible)
            return;
        
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
