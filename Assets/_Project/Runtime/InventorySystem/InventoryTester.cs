using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class InventoryTester : MMSingleton<InventoryTester>
{
    private PlayerInventory _playerInventory = new PlayerInventory();

    public PlayerInventory PlayerInventory => _playerInventory;

    protected void Start()
    {
        foreach (var itemData in ItemContainer.Instance.ItemDatas)
        {
            if (itemData.Value.StartQuantity >= 0)
            {
                _playerInventory.AddItem(itemData.Key, itemData.Value.StartQuantity);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _playerInventory.AddItem(ItemEnum.Grenade);
            _playerInventory.AddItem(ItemEnum.Flower);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            _playerInventory.RemoveItems(ItemEnum.Grenade, 1);
            _playerInventory.RemoveItems(ItemEnum.Flower, 1);
        }
    }
}
