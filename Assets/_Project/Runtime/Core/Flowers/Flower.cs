using UnityEngine;
using BeyondTales.InventorySystem;
using _Project.Runtime.Core.Herbalist;

public class Flower : MonoBehaviour, ITransformable
{
    [SerializeField] private ItemEnum _flowerType;

    public ItemEnum FlowerType => _flowerType;

    public Transform Transform => this.transform;

    public void Pick()
    {
        Destroy(gameObject);
    }

    public void SetFlowerType(ItemEnum flowerType)
    {
        _flowerType = flowerType;
    }
}
