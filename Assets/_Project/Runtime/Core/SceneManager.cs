using System;
using _Project.Runtime.Core.Interactables;
using _Project.Runtime.Core.SaveSystem;
using _Project.Runtime.Infrastructure.Factories;
using Zenject;

namespace _Project.Runtime.Core
{
    public enum Scene
    {
        Menu = 0, 
        Main = 1,
        MainCopyTestScreenplay = 2,
    }
    
    
    public class SceneManager : ISceneManager
    {

        private DontDestroyContainer _dontDestroyContainer;

        public event Action<Scene> OnSceneLoaded;
        [Inject]
        private void Construct(DontDestroyContainer dontDestroyContainer)
        {
            _dontDestroyContainer = dontDestroyContainer;
        }
        public void LoadScene(Scene scene)
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
            _dontDestroyContainer.ActivateBlackScreen(true);
            var asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.ToString());
            asyncOperation.completed += operation =>
            {
                OnSceneLoaded?.Invoke(scene);
                _dontDestroyContainer.ActivateScene(0.5f, scene);
            };
        }
    }

    public interface ISceneManager
    {
        public void LoadScene(Scene scene);

        public event Action<Scene> OnSceneLoaded;
    }
}