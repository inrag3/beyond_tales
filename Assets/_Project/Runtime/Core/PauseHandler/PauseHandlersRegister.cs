using System.Collections.Generic;
using UnityEngine;

namespace _Project.Runtime.Core.PauseHandler
{
    public class PauseHandlersRegister
    {
        private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

        public void RegisterPauseHandler(IPauseHandler handler)
        {
            _pauseHandlers.Add(handler);
        }
        
        public void UnregisterPauseHandler(IPauseHandler handler)
        {
            _pauseHandlers.Remove(handler);
        }

        public void PauseAll()
        {
            foreach (var handler in _pauseHandlers)
            {
                handler.Pause();
            }
        }

        public void ResumeAll()
        {
            foreach (var handler in _pauseHandlers)
            {
                handler.Resume();
            }
        }

        public void Reset()
        {
            _pauseHandlers.Clear();
        }
    }
}