using System;
using _Project.Runtime.Meta.Health;
using R3;
using Zenject;

namespace _Project.Runtime.Core.Health
{
    public class HealthPresenter : IInitializable, IDisposable
    {
        private readonly IReadOnlyHealth _health;
        private readonly IHealthView _view;
        private IDisposable _subscription;

        private HealthPresenter(IReadOnlyHealth health, IHealthView view)
        {
            _view = view;
            _health = health;
        }

        public void Initialize()
        {
            _subscription = _health.Value.Subscribe(OnHealthChanged);
        }

        private void OnHealthChanged(float value)
        {
            _view.SetHealth(value);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}