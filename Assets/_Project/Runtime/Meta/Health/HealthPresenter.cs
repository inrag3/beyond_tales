using System;
using _Project.Runtime.Core.Health;
using R3;
using Zenject;

namespace _Project.Runtime.Meta.Health
{
    public sealed class HealthPresenter : IInitializable, IDisposable
    {
        private readonly IHealth _health;
        private readonly HealthView _view;
        private IDisposable _subscription;
        public HealthPresenter(HealthView view, IHealth health)
        {
            _health = health;
            _view = view;
        }
        public void Initialize()
        {
            _subscription = _health.Value.Subscribe(OnHealthChanged);
        }

        private void OnHealthChanged(float value)
        {
            float result = value * 1f / _health.MaxValue;
            _view.SetHealth(result);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}