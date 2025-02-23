using UnityEngine;
using System.Linq;
using Zenject;
using _Project.Runtime.Core.Interactables;

public class BedsObserver : MonoBehaviour
{
    private Bed[] _beds;
    private Door[] _doors;

    [Inject]
    private void Construct(Bed[] beds, Door[] doors)
    {
        _beds = beds;
        _doors = doors;
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
        var closedDoors = _doors.Where(door => door.IsOpen == false).ToList();
        foreach (var door in closedDoors)
        {
            door.SwitchState();
        }
    }

    private void OnDestroy()
    {
        foreach (var bed in _beds)
        {
            bed.OnBedCompleted -= OnBedCompleted;
        }
    }
}
