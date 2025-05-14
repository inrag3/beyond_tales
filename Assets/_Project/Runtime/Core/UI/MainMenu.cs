using System;
using _Project.Runtime.Core.SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Runtime.Core.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continueButton;
        
        private ISceneManager _sceneManager;
        private DontDestroyContainer _dontDestroyContainer;
        private SaveLoader _saveLoader;
        
        [Inject]
        private void Construct(ISceneManager sceneManager, DontDestroyContainer dontDestroyContainer, SaveLoader saveLoader)
        {
            _sceneManager = sceneManager;
            _dontDestroyContainer = dontDestroyContainer;
            _saveLoader = saveLoader;
        }

        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayPressed);
            _exitButton.onClick.AddListener(OnExitPressed);
            if (_saveLoader.HasSave())
            {
                _continueButton.interactable = true;
                _continueButton.onClick.AddListener(OnContinuePressed);
            }
            else
            {
                _continueButton.interactable = false;
            }
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayPressed);
            _exitButton.onClick.RemoveListener(OnExitPressed);
            _continueButton.onClick.RemoveListener(OnContinuePressed);
        }
        
        private void OnPlayPressed()
        {
            _sceneManager.LoadScene(Scene.MainCopyTestScreenplay);
        }
        
        private void OnExitPressed()
        {
            Application.Quit();
        }

        private void OnContinuePressed()
        {
            _dontDestroyContainer.RequireSaveLoad = true;
            _sceneManager.LoadScene(Scene.MainCopyTestScreenplay);
        }
    }
}
