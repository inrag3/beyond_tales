using System.Linq;
using _Project.Runtime.Core.Interactables;
using UnityEngine;
using Zenject;

public class BedsObserver : MonoBehaviour
{
    private Bed[] _beds;
    [SerializeField] private Door _door;

    [Inject]
    private void Construct(Bed[] beds)
    {
        _beds = beds;
    }

    private void Start()
    {
        foreach (var bed in _beds)
        {
            bed.OnBedCompleted += OnBedCompleted;
        }
    }

    private void OnBedCompleted()
    {
        if (_beds.All(bed => bed.IsCorrectFlowerPlanted))
        {
            OnAllBedsCompleted();
        }
    }

    private void OnAllBedsCompleted()
    {
        _door.Open();
    }

    private void OnDestroy()
    {
        foreach (var bed in _beds)
        {
            bed.OnBedCompleted -= OnBedCompleted;
        }
    }
}