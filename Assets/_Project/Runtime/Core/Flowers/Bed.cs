using UnityEngine;
using System;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.InventorySystem;

public class Bed : Interactable
{
    [SerializeField] private ItemEnum _requiredFlowerType;
    public ItemEnum RequiredFlowerType => _requiredFlowerType;
    public event Action OnBedCompleted;
    public override void Interact(IInteractableVisitor visitor)
    {
        if (!IsInteractable)
            return;
        
        visitor.Accept(this);
    }
    
    public void Plant(Flower flower)
    {
        IsInteractable = false;
        flower.transform.SetParent(transform);
        flower.transform.localPosition = Vector3.zero;
        flower.Plant();
        OnBedCompleted?.Invoke();
    }
}
