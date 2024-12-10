using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.InventorySystem;
using UnityEngine;

namespace _Project.Runtime.Core.Interactables
{
    
    public class Item : Interactable
    {
        [field: SerializeField] public ItemValue Data { get; private set;  }
        
        public override void Interact(IInteractableVisitor visitor)
        {
            IsInteractable = false; 
            Destroy(this);
            visitor.Accept(this);
        }
    }
}