using UnityEngine;
using System;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.InventorySystem;

public class Bed : MonoBehaviour
{
    [SerializeField] private ItemEnum _requiredFlowerType;
    public ItemEnum RequiredFlowerType => _requiredFlowerType;
    public event Action OnBedCompleted;

    private bool _flowerPlanted = false;

    public bool FlowerPlanted => _flowerPlanted;
    /*public override void Interact(IInteractableVisitor visitor)
    {
        if (!IsAccessible)
            return;
        
        visitor.Accept(this);
    }*/
    
    public void Plant(Flower flower)
    {
        //IsAccessible = false;
        _flowerPlanted = true;
        flower.transform.SetParent(transform);
        flower.transform.localPosition = Vector3.zero;
        flower.Plant();
        OnBedCompleted?.Invoke();
    }
}
