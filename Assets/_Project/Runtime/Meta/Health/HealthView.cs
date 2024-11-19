using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Runtime.Meta.Health
{
    public sealed class HealthView : MonoBehaviour, IHealthView
    {
        [SerializeField] private Image _image;
        public void SetHealth(float value)
        {
            _image.DOFillAmount(value, 0.125f);
        }
    }

    public interface IHealthView
    {
        public void SetHealth(float value);
    }
}