using _Project.Runtime.Meta.Health;
using UnityEngine;

namespace _Project.Runtime.Infrastructure.Factories.UI
{
    public interface IHealthViewFactory
    {
        public HealthView Create(Transform parent);
    }
    
    
        
    public interface IView
    {
        public RectTransform Transform { get; }
        public void Show();
        public void Hide();
    }
}