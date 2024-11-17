using System.Collections.Generic;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.Infrastructure.Installers.GameObject;
using UnityEngine;
using Zenject;
using BeyondTales.InventorySystem;

public class Picker : ITickable
{
    private IInputService _inputService;
    private IScanner _scanner;
    private IPlayerInventory _playerInventory;
    private IHerbalistProvider _herbalistProvider;

    [Inject]
    private void Construct(IInputService inputService, IScanner scanner, IHerbalistProvider herbalistProvider, IPlayerInventory playerInventory)
    {
        _herbalistProvider = herbalistProvider;
        _scanner = scanner;
        _inputService = inputService;
        _playerInventory = playerInventory;
    }

    public void Tick()
    {
        if (!_inputService.IsInteractButtonPressed)
            return;

        Vector3 origin = _herbalistProvider.Herbalist.Transform.position;
        float pickupRadius = 5.0f;

        var flowers = _scanner.Scan<Flower>(origin, pickupRadius);

        ITransformable closestFlower = flowers.Closest(origin);

        if (closestFlower is Flower flower)
        {
            flower.Pick();
            _playerInventory.AddItem(flower.FlowerType);
        }
    }
}
