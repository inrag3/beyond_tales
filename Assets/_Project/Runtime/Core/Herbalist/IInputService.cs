namespace _Project.Runtime.Core.Herbalist
{
    public interface IInputService
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public bool IsRollButtonPressed { get; }
    }
}