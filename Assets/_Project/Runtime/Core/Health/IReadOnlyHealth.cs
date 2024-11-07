using R3;

namespace _Project.Runtime.Core.Health
{
    public interface IReadOnlyHealth
    {
        public ReadOnlyReactiveProperty<int> Value { get; }
    }
}