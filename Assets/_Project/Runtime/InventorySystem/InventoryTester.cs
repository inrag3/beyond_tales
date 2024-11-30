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
        if (Input.GetKeyDown(KeyCode.L))
        {
            _playerInventory.AddItem(ItemEnum.Grenade);
            _playerInventory.AddItem(ItemEnum.Flower);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            _playerInventory.RemoveItem(ItemEnum.Grenade, 1);
            _playerInventory.RemoveItem(ItemEnum.Flower, 1);
        }
    }
}