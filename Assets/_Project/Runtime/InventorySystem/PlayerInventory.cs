using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using Zenject;

public class PlayerInventory : IPlayerInventory, IInitializable
{
    private readonly Dictionary<ItemEnum, int> _items = new();
    private IItemContainer _itemContainer;

    public IReadOnlyDictionary<ItemEnum, int> Items => _items;

    public event Action<ItemEnum, int, int> OnChangeInventoryItemCount;

    [Inject]
    public void Construct(IItemContainer itemContainer)
    {
        _itemContainer = itemContainer;
    }

    public void Initialize()
    {
        foreach (var itemData in _itemContainer.ItemData)
        {
            if (itemData.Value.StartQuantity >= 0)
            {
                AddItem(itemData.Key, itemData.Value.StartQuantity);
            }
        }
    }

    public void AddItem(ItemEnum itemEnum, int quantity = 1)
    {
        var itemData = _itemContainer.ItemData[itemEnum];
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

    public bool RemoveItem(ItemEnum itemEnum, int quantity)
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

    public bool HasItems(ItemQuantityPair[] its)
    {
        foreach (var itPair in its)
        {
            if (GetItemCount(itPair.itemType) < itPair.quantity)
            {
                return false;
            }
        }

        return true;
    }

    public bool CanAddItems(ItemQuantityPair[] its, bool addIfPossible)
    {
        foreach (var itPair in its)
        {
            int maxCount = _itemContainer.ItemData[itPair.itemType].MaxStackSize;
            if (maxCount < 0)
            {
                continue;
            }

            if (GetItemCount(itPair.itemType) + itPair.quantity > maxCount)
            {
                return false;
            }
        }

        if (addIfPossible)
        {
            AddItems(its);
        }

        return true;
    }

    public void AddItems(ItemQuantityPair[] its)
    {
        foreach (var itPair in its)
        {
            AddItem(itPair.itemType, itPair.quantity);
        }
    }

    public void RemoveItems(ItemQuantityPair[] its)
    {
        foreach (var itPair in its)
        {
            RemoveItem(itPair.itemType, itPair.quantity);
        }
    }
}

public interface IPlayerInventory
{
    IReadOnlyDictionary<ItemEnum, int> Items { get; }

    event Action<ItemEnum, int, int> OnChangeInventoryItemCount;
    void AddItem(ItemEnum itemEnum, int quantity = 1);
    int GetItemCount(ItemEnum itemEnum);
    bool RemoveItem(ItemEnum itemEnum, int quantity);

    public bool CanAddItems(ItemQuantityPair[] its, bool AddIfPossible);

    public bool HasItems(ItemQuantityPair[] its);
    public void AddItems(ItemQuantityPair[] its);

    public void RemoveItems(ItemQuantityPair[] its);
}

[Serializable]
public class ItemQuantityPair
{
    public ItemEnum itemType;
    public int quantity;
}