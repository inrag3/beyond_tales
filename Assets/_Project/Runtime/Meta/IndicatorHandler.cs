using System.Collections.Generic;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Infrastructure.Factories.UI;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Meta
{
    public class IndicatorHandler : IInitializable, ITickable
    {
        private readonly Dictionary<ITransformable, IView> _indicators = new();
        private Camera _camera;

        public void Initialize()
        {
            _camera = Camera.main;
        }

        public void Register(ITransformable transform, IView view)
        {
            _indicators.Add(transform, view);
            view.Show();
        }

        public void Unregister(ITransformable transform)
        {
            if (!_indicators.TryGetValue(transform, out IView view))
                return;
            
            view.Hide();
            _indicators.Remove(transform);
        }

        public void Tick()
        {
            foreach (var pair in _indicators)
            {
                ITransformable target = pair.Key;
                IView view = pair.Value;
                Vector3 targetScreenPosition = _camera.WorldToScreenPoint(target.Transform.position);
                view.Transform.transform.position = targetScreenPosition;
            }
        }
    }
}