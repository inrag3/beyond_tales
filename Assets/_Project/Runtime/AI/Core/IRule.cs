namespace _Project.Runtime.AI.Core
{
    public interface IRule
    {
        public bool IsExecutable { get; }

        public void Execute();
    }
}