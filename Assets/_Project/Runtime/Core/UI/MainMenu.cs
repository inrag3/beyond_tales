using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Runtime.Core.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        
        private ISceneManager _sceneManager;
        
        [Inject]
        private void Construct(ISceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayPressed);
            _exitButton.onClick.AddListener(OnExitPressed);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayPressed);
            _exitButton.onClick.RemoveListener(OnExitPressed);
        }
        
        private void OnPlayPressed()
        {
            _sceneManager.LoadScene(Scene.MainCopyTestScreenplay);
        }
        
        private void OnExitPressed()
        {
            Application.Quit();
        }
    }
}
