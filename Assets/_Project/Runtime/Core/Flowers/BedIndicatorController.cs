using UnityEngine;
using _Project.Runtime.InventorySystem;

public class BedIndicatorController : MonoBehaviour
{
    [Header("—сылка на гр€дку")]
    [SerializeField] private Bed bed;

    [Header("—сылка на Renderer сферы")]
    [SerializeField] private Renderer sphereRenderer;

    private void Awake()
    {
        if (bed == null)
        {
            bed = GetComponentInParent<Bed>();
        }
    }

    private void Start()
    {
        if (bed != null)
        {
            bed.OnBedCompleted += OnBedCompleted;
        }
        else
        {
            Debug.LogWarning($"Bed не найден");
        }
    }

    private void OnDestroy()
    {
        if (bed != null)
        {
            bed.OnBedCompleted -= OnBedCompleted;
        }
    }

    private void OnBedCompleted()
    {
        Destroy(gameObject);
    }
}
