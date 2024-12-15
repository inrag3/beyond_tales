using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.InventorySystem;
using UnityEngine;

namespace _Project.Runtime.Core.Interactables.Items
{
    public class Item : Interactable
    {
        [field: SerializeField] public ItemValue Data { get; private set;  }
        
        public override void Interact(IInteractableVisitor visitor)
        {
            IsAccessible = false; 
            Destroy(gameObject);
            visitor.Accept(this);
        }
    }
}