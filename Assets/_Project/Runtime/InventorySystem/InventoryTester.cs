using _Project.Runtime.InventorySystem;
using UnityEngine;
using Zenject;

public class InventoryTester : ITickable
{
    private IPlayerInventory _playerInventory;

    public IPlayerInventory PlayerInventory => _playerInventory;

    [Inject]
    private InventoryTester(IPlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
    }


    public void Tick()
    {
       
    }
}