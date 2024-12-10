using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.InventorySystem;
using UnityEngine;

namespace _Project.Runtime.Core.Interactables
{
    
    public class Item : Interactable
    {
        [field: SerializeField] public ItemValue Value { get; private set;  }
        
        public override void Interact(IInteractableVisitor visitor)
        {
            Destroy(this);
            visitor.Accept(this);
        }
    }
}