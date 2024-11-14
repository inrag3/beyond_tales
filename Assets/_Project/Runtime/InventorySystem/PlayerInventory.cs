using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree.Util;
using UnityEngine;

public class PlayerInventory
{
    private Dictionary<ItemEnum, int> _items = new Dictionary<ItemEnum, int>();

    public IReadOnlyDictionary<ItemEnum, int> Items => _items;

    public event Action<ItemEnum, int, int> OnChangeInventoryItemCount; 
    public void AddItem(ItemEnum itemEnum, int quantity = 1)
    {
        var itemData = ItemContainer.Instance.GetItemData(itemEnum);
        if (_items.TryGetValue(itemEnum, out var quant))
        {
            if (itemData.MaxStackSize < 0 || quantity + quant <= itemData.MaxStackSize)
            {
                _items[itemEnum] = quantity + quant;
                OnChangeInventoryItemCount?.Invoke(itemEnum, quant, quantity + quant);
            }
            else
            {
                _items[itemEnum] = itemData.MaxStackSize;
                OnChangeInventoryItemCount?.Invoke(itemEnum, quant, itemData.MaxStackSize);
            }
        }
        else
        {
            if (itemData.MaxStackSize < 0 || quantity < itemData.MaxStackSize)
            {
                _items[itemEnum] = quantity;
            }
            else
            {
                _items[itemEnum] = itemData.MaxStackSize;
            }
            
            OnChangeInventoryItemCount?.Invoke(itemEnum, 0, _items[itemEnum]);
        }
    }

    public int GetItemCount(ItemEnum itemEnum)
    {
        if (_items.TryGetValue(itemEnum, out var quant))
        {
            return quant;
        }

        return 0;
    }

    public bool RemoveItems(ItemEnum itemEnum, int quantity)
    {
        if (_items.TryGetValue(itemEnum, out var quant))
        {
            if (quant >= quantity)
            {
                _items[itemEnum] -= quantity;
                OnChangeInventoryItemCount?.Invoke(itemEnum, quant, quant - quantity);
                return true;
            }

            return false;
        }

        return false;
    }

}
