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
    private Material _flowerMaterial;

    public bool IsComplete => _isComplete;
    public ItemEnum RequiredFlowerType => _requiredFlowerType;

    public event Action OnBedCompleted;

    public Transform Transform => this.transform;

    public void Plant(Flower flower)
    {
        _plantedFlower = flower;
        _flowerMaterial = flower.FlowerMaterial;
        _plantedFlower.transform.SetParent(this.transform);
        _plantedFlower.transform.localPosition = Vector3.zero;
        ApplyMaterialToPlantedFlower();
        _isComplete = true;
        flower.IsPlanted = true;

        OnBedCompleted?.Invoke();
    }

    private void ApplyMaterialToPlantedFlower()
    {
        var renderer = _plantedFlower.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = _flowerMaterial;
        }
    }
}
