using System.Collections.Generic;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Infrastructure.Factories.UI;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Infrastructure.Installers.SceneInstallers
{
    public class IndicatorHandler : IInitializable, ILateTickable
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
            var view = _indicators[transform];
            view.Hide();
            _indicators.Remove(transform);
        }

        public void LateTick()
        {
            foreach (var pair in _indicators)
            {
                var target = pair.Key;
                var view = pair.Value;
                var targetScreenPosition = _camera.WorldToScreenPoint(target.Transform.position);
                view.Transform.transform.position = targetScreenPosition;
            }
        }
    }
}