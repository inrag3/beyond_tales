using _Project.Runtime.Audio;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.PauseHandler;
using _Project.Runtime.Infrastructure.Factories;
using Extensions;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core
{
    public class VictoryHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _uiHolder;
        
        private ISceneManager _sceneManager;
        private IHerbalistProvider _herbalistProvider;
        private DontDestroyContainer _dontDestroyContainer;
        private AudioService _audioService;
        private SoundSettings _soundSettings;
        private IHealth _health;
        private PlayerInventory _playerInventory;
        private PauseHandlersRegister _pauseHandler;

        private bool _subscribed;
        
        [Inject]
        private void Construct(IHerbalistProvider herbalistProvider, 
            ISceneManager sceneManager, DontDestroyContainer dontDestroyContainer, 
            AudioService audioService, SoundSettings soundSettings, IHealth health, 
            PlayerInventory playerInventory, PauseHandlersRegister pauseHandler)
        {
            _herbalistProvider = herbalistProvider;
            _sceneManager = sceneManager;
            _dontDestroyContainer = dontDestroyContainer;
            _audioService = audioService;
            _soundSettings = soundSettings;
            _health = health;
            _playerInventory = playerInventory;
            _pauseHandler = pauseHandler;
        }

        public void ShowVictory()
        {
            _audioService.ChangeMusic(_soundSettings.peacefulMusic);
            _uiHolder.gameObject.SetActive(true);
            foreach (var creature in GameObject.FindObjectsByType<Creature>(FindObjectsInactive.Include,FindObjectsSortMode.None))
            {
                Destroy(creature.gameObject);
            }
        }



        public void RestartGame()
        {
            PrepareForUnloadScene();
            _sceneManager.LoadScene(Scene.MainCopyTestScreenplay);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void PrepareForUnloadScene()
        {
            _pauseHandler.Reset();
            foreach (var creature in GameObject.FindObjectsByType<Creature>(FindObjectsInactive.Include,FindObjectsSortMode.None))
            {
                Destroy(creature.gameObject);
            }
            Destroy(_herbalistProvider.Herbalist.GameObject);
            _health.Reset();
            _playerInventory.Reset();
        }
    }
}