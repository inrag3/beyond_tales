using _Project.Runtime.Core.PauseHandler;

namespace _Project.Runtime.Infrastructure.Factories
{
    public interface ISpawner : IPauseHandler
    {
        public void Begin();
    }
}