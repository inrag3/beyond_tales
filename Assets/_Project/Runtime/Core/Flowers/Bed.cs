using UnityEngine;
using System;
using BeyondTales.InventorySystem;
using _Project.Runtime.Core.Herbalist;
using System.Collections.Generic;

public class Bed : MonoBehaviour, ITransformable
{
    [SerializeField] private ItemEnum _requiredFlowerType;
    private Flower _plantedFlower;
    private bool _isComplete;

    public bool IsComplete => _isComplete;
    public ItemEnum RequiredFlowerType => _requiredFlowerType;

    public event Action OnBedCompleted;

    public Transform Transform => this.transform;

    public void Plant(Flower flower)
    {
        _plantedFlower = flower;
        _plantedFlower.transform.SetParent(this.transform);
        _plantedFlower.transform.localPosition = Vector3.zero;
        _isComplete = true;

        OnBedCompleted?.Invoke();
    }
}
