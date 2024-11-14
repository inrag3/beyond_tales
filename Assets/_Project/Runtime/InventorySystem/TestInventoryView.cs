using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TestInventoryView : MonoBehaviour
{
    [SerializeField] private Text _text;

    private void Start()
    {
        OnChangeInventory(default, default,default);
        InventoryTester.Instance.PlayerInventory.OnChangeInventoryItemCount += OnChangeInventory;
    }

    private void OnChangeInventory(ItemEnum itemEnum, int prev, int cur)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var kv in InventoryTester.Instance.PlayerInventory.Items)
        {
            sb.Append($"{kv.Key}: {kv.Value}\n");
        }

        _text.text = sb.ToString();
    }

    private void OnDestroy()
    {
        InventoryTester.Instance.PlayerInventory.OnChangeInventoryItemCount -= OnChangeInventory;
    }
}
