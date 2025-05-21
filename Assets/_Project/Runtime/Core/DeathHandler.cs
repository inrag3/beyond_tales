using System;
using _Project.Runtime.Audio;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.SaveSystem;
using _Project.Runtime.Infrastructure.Factories;
using Extensions;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _uiHolder;

        private ISceneManager _sceneManager;
        private IHerbalistProvider _herbalistProvider;
        private DontDestroyContainer _dontDestroyContainer;
        private AudioService _audioService;
        private SoundSettings _soundSettings;

        private bool _subscribed;
        
        [Inject]
        private void Construct(IHerbalistProvider herbalistProvider, 
            ISceneManager sceneManager, DontDestroyContainer dontDestroyContainer, 
            AudioService audioService, SoundSettings soundSettings)
        {
            _herbalistProvider = herbalistProvider;
            _sceneManager = sceneManager;
            _dontDestroyContainer = dontDestroyContainer;
            _audioService = audioService;
            _soundSettings = soundSettings;
        }

        private void Update()
        {
            if (!_subscribed)
            {
                if (!_herbalistProvider.Herbalist.IsNullOrDestroyed())
                {
                    _herbalistProvider.Herbalist.OnDeath += OnPlayerDeath;
                    _subscribed = true;
                }
            }
        }

        private void OnPlayerDeath()
        {
            _audioService.ChangeMusic(_soundSettings.peacefulMusic);
            _uiHolder.SetActive(true);
        }

        public void RestartGame()
        {
            PrepareForUnloadScene();
            _sceneManager.LoadScene(Scene.MainCopyTestScreenplay);
        }

        public void ReloadGame()
        {
            PrepareForUnloadScene();
            _dontDestroyContainer.RequireSaveLoad = true;
            _sceneManager.LoadScene(Scene.MainCopyTestScreenplay);
        }

        private void PrepareForUnloadScene()
        {
            foreach (var creature in GameObject.FindObjectsByType<Creature>(FindObjectsInactive.Include,FindObjectsSortMode.None))
            {
                Destroy(creature.gameObject);
            }
            Destroy(_herbalistProvider.Herbalist.GameObject);
            
        }
    }
}