using System;
using BeyondTales.InventorySystem;
using UnityEngine;

namespace _Project.Runtime.InventorySystem
{
    [Serializable]
    public struct ItemValue
    {
        [field: SerializeField] public ItemEnum ItemEnum { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}