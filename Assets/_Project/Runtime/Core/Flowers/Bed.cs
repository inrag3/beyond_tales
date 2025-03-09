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
    public event Action OnBedReset;

    private bool _isAccessible = true;
    public bool IsAccessible
    {
        get => _isAccessible;
        set
        {
            _isAccessible = value;
        }
    }
    public bool IsCompleted { get; private set; } = false;

    private bool _flowerPlanted = false;
    public bool FlowerPlanted => _flowerPlanted;

    private Flower _plantedFlower;
    public bool IsCorrectFlowerPlanted { get; private set; } = false;

    public void Plant(Flower flower)
    {
        if (_flowerPlanted) { return; }

        _flowerPlanted = true;
        _plantedFlower = flower;
        flower.transform.SetParent(transform);
        flower.transform.localPosition = Vector3.zero;

        if (flower.FlowerType == _requiredFlowerType)
        {
            IsCorrectFlowerPlanted = true;
            IsCompleted = true;
            flower.Plant();
            OnBedCompleted?.Invoke();
            IsAccessible = true;
        }
        else
        {
            IsCorrectFlowerPlanted = false;
            IsAccessible = true;
        }
    }

    public void Unplant()
    {
        if (!_flowerPlanted || IsCorrectFlowerPlanted) { return; }

        if (_plantedFlower != null)
        {
            _plantedFlower.transform.SetParent(null);
            _plantedFlower.IsAccessible = true;
        }
        _flowerPlanted = false;
        _plantedFlower = null;
        IsCorrectFlowerPlanted = false;
        IsCompleted = false;

        IsAccessible = true;
        OnBedReset?.Invoke();
    }
}
