using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemContainer : IItemContainer, IInitializable
{
    private const string ItemsLocationPath = "Items";

    private Dictionary<ItemEnum, ItemData> _itemData;

    public IReadOnlyDictionary<ItemEnum, ItemData> ItemData => _itemData;

    public void Initialize()
    {
        _itemData = new Dictionary<ItemEnum, ItemData>();
        foreach (var itemData in Resources.LoadAll<ItemData>(ItemsLocationPath))
        {
            _itemData.Add(itemData.ItemEnum, itemData);
        }
    }
}

public interface IItemContainer
{
    IReadOnlyDictionary<ItemEnum, ItemData> ItemData { get; }
}