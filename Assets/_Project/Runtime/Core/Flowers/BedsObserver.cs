using UnityEngine;
using System.Linq;

public class BedsObserver : MonoBehaviour
{
    [SerializeField] private Bed[] _beds;

    private void Start()
    {
        foreach (var bed in _beds)
        {
            bed.OnBedCompleted += OnBedCompleted;
        }
    }


    private void OnBedCompleted()
    {
        if (_beds.All(bed => bed.IsComplete))
        {
            OnAllBedsCompleted();
        }
    }

    private void OnAllBedsCompleted()
    {
        Debug.Log("Все грядки заполнены");
    }
}
