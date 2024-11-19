using System;
using _Project.Runtime.Config;
using R3;

namespace _Project.Runtime.Core.Health
{
    public class Health : IHealth, IReadOnlyHealth
    {
        private readonly ReactiveProperty<int> _value = new();

        public Health(IHealthConfig config)
        {
            MaxValue = config.MaxValue;
            _value.Value = MaxValue;
        }
        public ReadOnlyReactiveProperty<int> Value => _value;
        public int MaxValue { get; }

        public void Increase(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Value must be greater than 0", nameof(value));

            _value.Value = Math.Clamp(_value.Value + value, 0, MaxValue);
        }
        public void Decrease(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Value must be greater than 0", nameof(value));

            _value.Value = Math.Clamp(_value.Value - value, 0, MaxValue);
        }
    }
}