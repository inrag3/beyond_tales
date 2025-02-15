using UnityEngine;
using System.Linq;
using Zenject;

public class BedsObserver : MonoBehaviour
{
    private Bed[] _beds;

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
        if (_beds.All(bed => bed.FlowerPlanted))
        {
            OnAllBedsCompleted();
        }
    }
    
    private void OnAllBedsCompleted()
    {
        Debug.Log("Complete!");
    }
    
    private void OnDestroy()
    {
        foreach (var bed in _beds)
        {
            bed.OnBedCompleted -= OnBedCompleted;
        }
    }
}
