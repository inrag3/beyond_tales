using R3;

namespace _Project.Runtime.Core.Health
{
    public interface IHealth
    {
        public ReadOnlyReactiveProperty<int> Value { get; }
        int MaxValue { get; }

        public void Increase(int value);

        public void Decrease(int value);
    }
}