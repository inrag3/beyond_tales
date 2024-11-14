using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class ItemContainer : MMSingleton<ItemContainer>
{
    private Dictionary<ItemEnum, ItemData> _itemDatas;

    public IReadOnlyDictionary<ItemEnum, ItemData> ItemDatas => _itemDatas;
    protected override void Awake()
    {
        base.Awake();
        _itemDatas = new Dictionary<ItemEnum, ItemData>();
        foreach (var itemData in Resources.LoadAll<ItemData>("Items"))
        {
            _itemDatas.Add(itemData.ItemEnum, itemData);
        }
        
    }

    public ItemData GetItemData(ItemEnum itemEnum)
    {
        return _itemDatas[itemEnum];
    }
}
