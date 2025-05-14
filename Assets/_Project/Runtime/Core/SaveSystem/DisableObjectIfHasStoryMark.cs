using System;
using UnityEngine;

namespace _Project.Runtime.Core.SaveSystem
{
    public class DisableObjectIfHasStoryMark : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDisable;
        [SerializeField] private string _storyMark;
        public string StoryMark => _storyMark;

        public void Activate()
        {
            _objectToDisable.SetActive(false);
        }
    }
}