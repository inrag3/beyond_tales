using System.Collections.Generic;

namespace _Project.Runtime.Core.PauseHandler
{
    public class PauseHandler : IPauseHandler
    {
        private readonly HashSet<IPauseHandler> _handlers = new();
    
        public void Register(IPauseHandler pauseHandler)
        {
            _handlers.Add(pauseHandler);
        }

        public void Unregister(IPauseHandler pauseHandler)
        {
            if (_handlers.Contains(pauseHandler))
            {
                _handlers.Remove(pauseHandler);
            }
        }

        public void Pause()
        {
            foreach (var handler in _handlers)
            {
                handler.Pause();
            }
        }
        public void Resume()
        {
            foreach (var handler in _handlers)
            {
                handler.Resume();
            }
        }
    }
}