using System.Linq;
using _Project.Runtime.Audio;
using _Project.Runtime.Core.Interactables;
using UnityEngine;
using Zenject;

public class BedsObserver : MonoBehaviour
{
    private Bed[] _beds;
    [SerializeField] private Door _door;

    private SoundSettings _soundSettings;
    private IAudioService _audioService;
    [Inject]
    private void Construct(Bed[] beds, SoundSettings soundSettings, IAudioService audioService)
    {
        _beds = beds;
        _soundSettings = soundSettings;
        _audioService = audioService;
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
        _audioService.PlayOneShot(_soundSettings.solvePuzzleClip);
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