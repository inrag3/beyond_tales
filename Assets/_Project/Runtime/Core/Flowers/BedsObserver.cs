using System;
using System.Linq;
using _Project.Runtime.Audio;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.QuestSystem;
using UnityEngine;
using Zenject;

public class BedsObserver : MonoBehaviour
{
    private Bed[] _beds;
    [SerializeField] private Door _door;
    [SerializeField] private SaveGameQuestAction _saveGameQuestAction;

    private IHerbalistProvider _herbalistProvider;
    private SoundSettings _soundSettings;
    private IAudioService _audioService;
    private IFlowerFactory _flowerFactory;
    
    private bool _updateFlag = false;

    private bool _puzzleCompleted;
    public bool PuzzleCompleted => _puzzleCompleted;

    [Inject]
    private void Construct(Bed[] beds, SoundSettings soundSettings, IAudioService audioService, 
        IFlowerFactory flowerFactory, IHerbalistProvider herbalistProvider)
    {
        _beds = beds;
        _soundSettings = soundSettings;
        _audioService = audioService;
        _flowerFactory = flowerFactory;
        _herbalistProvider = herbalistProvider;
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

    private void Update()
    {
        if (_updateFlag && _herbalistProvider.Herbalist.PlayerData.HasStoryMarks(new []{"PlantWatered"}))
        {
            _puzzleCompleted = true;
            _herbalistProvider.Herbalist.PlayerData.AddStoryMarks(new []{"PlantPuzzleSolved"});
            _audioService.PlayOneShot(_soundSettings.solvePuzzleClip);
            _door.Open();
            _saveGameQuestAction.Activate();
            _updateFlag = false;
        }
    }

    private void OnAllBedsCompleted()
    {
        _updateFlag = true;
    }

    public void CompletePuzzle()
    {
        foreach (var bed in _beds)
        {
            bed.Plant(_flowerFactory.Create(bed.RequiredFlowerType));
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