using _Project.Runtime.Infrastructure.Factories.UI;
using DG.Tweening;
using UnityEngine;

namespace _Project.Runtime.Meta
{
    public sealed class HintView : MonoBehaviour, IView
    {
        [SerializeField] private float _duration = 0.15f;
        
        public RectTransform Transform { get; private set; }
        private void Awake()
        {
            Transform = GetComponent<RectTransform>();
            transform.localScale = Vector3.zero;
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
}