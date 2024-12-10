using System;
using _Project.Runtime.Infrastructure.Factories.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Runtime.Meta.Health
{
    public sealed class HealthView : MonoBehaviour, IHealthView
    {
        [SerializeField] private Image _image;

        private void Awake()
        {
            Transform = GetComponent<RectTransform>();
        }
        public RectTransform Transform { get; private set; }
        public void SetHealth(float value)
        {
            _image.DOFillAmount(value, 0.125f);
        }

        public void Show()
        {
        }

        public void Hide()
        {
        }
    }

    public interface IHealthView : IView
    {
        public void SetHealth(float value);
    }
}