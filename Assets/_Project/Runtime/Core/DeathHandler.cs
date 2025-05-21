using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.SaveSystem;
using _Project.Runtime.Infrastructure.Factories;
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
        
        [Inject]
        private void Construct(IHerbalistProvider herbalistProvider, ISceneManager sceneManager, DontDestroyContainer dontDestroyContainer)
        {
            _herbalistProvider = herbalistProvider;
            _sceneManager = sceneManager;
            _dontDestroyContainer = dontDestroyContainer;
            _herbalistProvider.Herbalist.OnDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            _uiHolder.SetActive(true);
        }

        public void RestartGame()
        {
            _sceneManager.LoadScene(Scene.MainCopyTestScreenplay);
        }

        public void ReloadGame()
        {
            _dontDestroyContainer.RequireSaveLoad = true;
            _sceneManager.LoadScene(Scene.MainCopyTestScreenplay);
        }
    }
}