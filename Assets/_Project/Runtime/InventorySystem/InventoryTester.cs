using UnityEngine;
using Zenject;
using BeyondTales.InventorySystem;

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
        if (Input.GetKeyDown(KeyCode.L))
        {
            _playerInventory.AddItem(ItemEnum.Grenade);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            _playerInventory.RemoveItems(new ItemQuantityPair[]{ new(){itemType = ItemEnum.Grenade,quantity = 1 }});
        }
    }
}