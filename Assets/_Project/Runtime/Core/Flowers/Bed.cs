using UnityEngine;
using System;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.Interactables.Processors;
using _Project.Runtime.InventorySystem;
using Zenject;

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

    private IPlayerInventory _playerInventory;
    public bool IsCorrectFlowerPlanted
    {
        get
        {
            if (!_flowerPlanted)
            {
                //Debug.Log($"Flower isnt planted!");
                return false;
            }

            //Debug.Log($"flower type = {_plantedFlower.FlowerType} req type = {_requiredFlowerType}");
            return _plantedFlower.FlowerType == _requiredFlowerType;
        }
    }
    
    [Inject]
    public void Construct(IPlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
    }


    public void Plant(Flower flower)
    {
        if (_flowerPlanted) { return; }

        _flowerPlanted = true;
        _plantedFlower = flower;
        flower.transform.SetParent(transform);
        flower.transform.localPosition = Vector3.zero;
        
        //Debug.Log($"correct flower = {IsCorrectFlowerPlanted}");

        if (flower.FlowerType == _requiredFlowerType)
        {
            IsCompleted = true;
            flower.Plant();
            OnBedCompleted?.Invoke();
            IsAccessible = true;
        }
        else
        {
            flower.Plant();
            IsAccessible = true;
        }
    }

    public void Unplant()
    {
        if (!_flowerPlanted || IsCorrectFlowerPlanted) { return; }

        if (_plantedFlower != null)
        {
            /*_plantedFlower.transform.SetParent(null);
            _plantedFlower.IsAccessible = true;*/
            _playerInventory.AddItem(_plantedFlower.FlowerType);
            Destroy(_plantedFlower.gameObject);
        }
        _flowerPlanted = false;
        _plantedFlower = null;
        IsCompleted = false;

        IsAccessible = true;
        OnBedReset?.Invoke();
    }
}
