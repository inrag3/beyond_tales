using UnityEngine;
using Zenject;
using BeyondTales.InventorySystem;
using _Project.Runtime.Core.Herbalist;
using System.Collections.Generic;

using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.Infrastructure.Installers.GameObject;
using System.Linq;

public class Placer : ITickable
{
    private IInputService _inputService;
    private IPlayerInventory _playerInventory;
    private IHerbalistProvider _herbalistProvider;
    private Bed[] _allBeds;

    [Inject]
    public void Construct(IInputService inputService, IPlayerInventory playerInventory, IHerbalistProvider herbalistProvider, Bed[] allBeds)
    {
        _inputService = inputService;
        _playerInventory = playerInventory;
        _herbalistProvider = herbalistProvider;
        _allBeds = allBeds;
    }

    public void Tick()
    {
        if (_inputService.IsInteractButtonPressed)
        {
            Vector3 origin = _herbalistProvider.Herbalist.Transform.position;

            float plantingRadius = 3.0f;

            List<Bed> bedList = new List<Bed>(_allBeds)
            .Where(bed => Vector3.Distance(origin, bed.Transform.position) <= plantingRadius)
            .ToList();

            Bed closestBed = bedList.Closest(origin);

            if (closestBed != null)
            {
                if (!closestBed.IsComplete)
                {
                    TryPlaceFlower(closestBed);
                }
            }
        }
    }

    private void TryPlaceFlower(Bed bed)
    {
        ItemEnum requiredFlowerType = bed.RequiredFlowerType;
        if (_playerInventory.GetItemCount(requiredFlowerType) > 0)
        {
            _playerInventory.RemoveItems(requiredFlowerType, 1);
            SpawnFlowerOnBed(bed, requiredFlowerType);
        }
    }

    private void SpawnFlowerOnBed(Bed bed, ItemEnum flowerType)
    {
        GameObject flowerPrefab = Resources.Load<GameObject>("Prefabs/Flower");
        if (flowerPrefab != null)
        {
            Material flowerMaterial = GetMaterialForFlowerType(flowerType);
            if (flowerMaterial == null)
            {
                return;
            }
            GameObject spawnedFlower = Object.Instantiate(flowerPrefab);
            Flower flower = spawnedFlower.GetComponent<Flower>();
            flower.SetFlowerType(flowerType, flowerMaterial);
            bed.Plant(flower);
        }
    }

    private Material GetMaterialForFlowerType(ItemEnum flowerType)
    {
        switch (flowerType)
        {
            case ItemEnum.RedFlower:
                return Resources.Load<Material>("Prefabs/Materials/Red");
            case ItemEnum.BlueFlower:
                return Resources.Load<Material>("Prefabs/Materials/Blue");
            case ItemEnum.YellowFlower:
                return Resources.Load<Material>("Prefabs/Materials/Yellow");
            default:
                return null;
        }
    }
}