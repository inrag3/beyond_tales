using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using BeyondTales.InventorySystem;

public class TestInventoryView : MonoBehaviour
{
    [SerializeField] private Text _text;
    private IPlayerInventory _playerInventory;

    [Inject]
    private void Construct(IPlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
    }


    private void Awake()
    {
        OnChangeInventory(default, default, default);
        _playerInventory.OnChangeInventoryItemCount += OnChangeInventory;
    }

    private void OnChangeInventory(ItemEnum itemEnum, int prev, int cur)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var kv in _playerInventory.Items)
        {
            sb.Append($"{kv.Key}: {kv.Value}\n");
        }

        _text.text = sb.ToString();
    }

    private void OnDestroy()
    {
        _playerInventory.OnChangeInventoryItemCount -= OnChangeInventory;
    }
}