using System;
using UnityEngine;
using BeyondTales.InventorySystem;

[CreateAssetMenu(fileName = "new Item", menuName = "Item")]
public class ItemData : ScriptableObject, IEquatable<ItemData>
{
    [SerializeField] private ItemEnum _itemEnum;

    [Tooltip("Максимальное количество предметов этого типа которое может быть в инвентаре. " +
             "Если поставить отрицательное значение, то можно будет подбирать бесконечное количество")]
    [SerializeField]
    private int _maxStackSize;

    [Tooltip("Количество предметов этого типа, с которым игрок начинает игру." +
             "Если выставить 0, то предметов будет 0, но сам предмет будет инициализирован в инвентаре." +
             "Если выставить отрицательное значение, то такая инициализация происходить не будет")]
    [SerializeField]
    private int _startQuantity;

    public ItemEnum ItemEnum => _itemEnum;

    public int MaxStackSize => _maxStackSize;

    public int StartQuantity => _startQuantity;

    public bool Equals(ItemData other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Equals(_itemEnum, other._itemEnum);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ItemData)obj);
    }

    public override int GetHashCode()
    {
        return _itemEnum.GetHashCode();
    }
}