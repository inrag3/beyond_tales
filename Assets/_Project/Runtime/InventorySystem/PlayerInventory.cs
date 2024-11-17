using System;
using System.Collections.Generic;
using Zenject;
using BeyondTales.InventorySystem;

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
        if (_items.TryGetValue(itemEnum, out var currentQuantity))
        {
            if (itemData.MaxStackSize < 0 || currentQuantity + quantity <= itemData.MaxStackSize)
            {
                _items[itemEnum] += quantity;
            }
        }
        else
        {
            _items[itemEnum] = quantity;
        }

        OnChangeInventoryItemCount?.Invoke(itemEnum, currentQuantity, _items[itemEnum]);
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

public interface IPlayerInventory
{
    IReadOnlyDictionary<ItemEnum, int> Items { get; }

    event Action<ItemEnum, int, int> OnChangeInventoryItemCount;
    void AddItem(ItemEnum itemEnum, int quantity = 1);
    int GetItemCount(ItemEnum itemEnum);
    bool RemoveItems(ItemEnum itemEnum, int quantity);
}