using UnityEngine;

public class BedVisualController : MonoBehaviour
{
    [Header("Модели грядки")]
    [SerializeField] private GameObject emptyBedModel;
    [SerializeField] private GameObject fullBedModel;

    private Bed bed;

    private void Awake()
    {
        bed = GetComponent<Bed>();
        if (bed == null)
        {
            Debug.LogError("Компонент Bed не найден на " + gameObject.name);
        }
    }

    private void Start()
    {
        ShowEmptyModel();

        bed.OnBedCompleted += OnBedCompleted;
    }

    private void OnDestroy()
    {
        if (bed != null)
            bed.OnBedCompleted -= OnBedCompleted;
    }

    private void OnBedCompleted()
    {
        ShowFullModel();
    }

    private void ShowEmptyModel()
    {
        if (emptyBedModel != null)
            emptyBedModel.SetActive(true);
        if (fullBedModel != null)
            fullBedModel.SetActive(false);
    }

    private void ShowFullModel()
    {
        if (emptyBedModel != null)
            emptyBedModel.SetActive(false);
        if (fullBedModel != null)
            fullBedModel.SetActive(true);
    }
}
