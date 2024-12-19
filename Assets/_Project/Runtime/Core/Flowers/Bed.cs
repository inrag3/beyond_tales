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
        if (!IsAccessible)
            return;
        
        visitor.Accept(this);
    }
    
    public void Plant(Flower flower)
    {
        IsAccessible = false;
        flower.transform.SetParent(transform);
        flower.transform.localPosition = Vector3.zero;
        if (flower.Data.ItemEnum == _requiredFlowerType)
        {
            flower.Plant();
        }
        else
        {
            flower.Interacted += OnInteracted;
        }
        OnBedCompleted?.Invoke();
    }

    private void OnInteracted(Flower flower)
    {
        flower.Interacted -= OnInteracted;
        IsAccessible = true;
    }
}
