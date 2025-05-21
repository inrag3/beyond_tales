using System.Text;
using _Project.Runtime.InventorySystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestInventoryView : MonoBehaviour
{
    [SerializeField] private GameObject _blue;
    [SerializeField] private GameObject _red;
    [SerializeField] private GameObject _yellow;
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
        _blue.SetActive(_playerInventory.Items.ContainsKey(ItemEnum.BlueFlower) && _playerInventory.Items[ItemEnum.BlueFlower] > 0);
        _red.SetActive(_playerInventory.Items.ContainsKey(ItemEnum.RedFlower) && _playerInventory.Items[ItemEnum.RedFlower] > 0);
        _yellow.SetActive(_playerInventory.Items.ContainsKey(ItemEnum.YellowFlower) && _playerInventory.Items[ItemEnum.YellowFlower] > 0);
        // Debug.Log($"updated inventory blue:{_playerInventory.Items[ItemEnum.BlueFlower] > 0} red:{_playerInventory.Items[ItemEnum.RedFlower] > 0} yellow:{_playerInventory.Items[ItemEnum.YellowFlower] > 0}");
    }

    private void OnDestroy()
    {
        _playerInventory.OnChangeInventoryItemCount -= OnChangeInventory;
    }
}