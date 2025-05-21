using R3;

namespace _Project.Runtime.Core.Health
{
    public interface IHealth
    {
        public ReadOnlyReactiveProperty<float> Value { get; }
        int MaxValue { get; }

        public void Increase(float value);

        public void Decrease(float value);

        public void Reset();
    }
}