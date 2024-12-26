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
        [SerializeField] private Gradient _gradient;
        [SerializeField] private float _duration = 0.1f;
        private void Awake()
        {
            Transform = GetComponent<RectTransform>();
        }
        public RectTransform Transform { get; private set; }
        public void SetHealth(float value)
        {
            _image.DOFillAmount(value, 0.125f);
            Color gradient = _gradient.Evaluate(value);
            _image.DOColor(gradient, 0.125f);
        }

        public void Show()
        {
            Transform.DOScale(Vector3.one, _duration)
                .SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            Transform.DOScale(Vector3.zero, _duration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                });
        }
    }

    public interface IHealthView : IView
    {
        public void SetHealth(float value);
    }
}