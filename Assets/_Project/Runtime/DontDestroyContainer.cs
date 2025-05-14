using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Runtime
{
    public class DontDestroyContainer : MonoBehaviour
    {
        [SerializeField] private Image _blackScreen;
        private bool _requireSaveLoad;

        public event Action<Core.Scene> OnActivateScene;

        public bool RequireSaveLoad
        {
            get => _requireSaveLoad;
            set => _requireSaveLoad = value;
        }

        public void ActivateBlackScreen(bool activate)
        {
            _blackScreen.gameObject.SetActive(activate);
        }

        public void ActivateScene(float time, Core.Scene scene)
        {
            StartCoroutine(SceneDelayedActivation(time, scene));
        }

        private IEnumerator SceneDelayedActivation(float time, Core.Scene scene)
        {
            yield return new WaitForSeconds(time);
            
            OnActivateScene?.Invoke(scene);
            
            ActivateBlackScreen(false);
        }
    }
}