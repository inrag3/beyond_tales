using System;
using _Project.Runtime.Meta;
using _Project.Runtime.Meta.Health;
using R3;
using Zenject;

namespace _Project.Runtime.Core.Health
{
    public class HealthPresenter : IInitializable, IDisposable
    {
        private IReadOnlyHealth _health;
        private IDisposable _subscription;
        private IHealthView _view;

        private HealthPresenter(IReadOnlyHealth health, IHealthView view)
        {
            _view = view;
            _health = health;
        }

        public void Initialize()
        {
            _subscription = _health.Value.Subscribe(OnHealthChanged);
        }

        private void OnHealthChanged(int value)
        {
            _view.SetValue(value);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}