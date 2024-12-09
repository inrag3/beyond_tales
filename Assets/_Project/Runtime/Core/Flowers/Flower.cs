using UnityEngine;
using BeyondTales.InventorySystem;
using _Project.Runtime.Core.Herbalist;

public class Flower : MonoBehaviour, ITransformable
{
    [SerializeField] private ItemEnum _flowerType;
    [SerializeField] private Material _flowerMaterial;

    public ItemEnum FlowerType => _flowerType;
    public Material FlowerMaterial => _flowerMaterial;
    public Transform Transform => this.transform;

    public bool IsPlanted { get; set; }

    public void Pick()
    {
        Destroy(gameObject);
    }

    public void SetFlowerType(ItemEnum flowerType, Material material)
    {
        _flowerType = flowerType;
        _flowerMaterial = material;
        ApplyMaterial();
    }

    private void ApplyMaterial()
    {
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = _flowerMaterial;
        }
    }
}
