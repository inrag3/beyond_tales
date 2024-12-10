using System;
using UnityEngine;

[Serializable]
public struct ItemValue
{
    [field: SerializeField] public ItemEnum ItemEnum { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
}