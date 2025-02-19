using System.Collections.Generic;
using _Project.Runtime.Core.PauseHandler;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core
{
    public class Waver : IPauseHandler, ITickable
    {
        private readonly List<ISpawner> _spawners;
        public Waver(List<ISpawner> spawners)
        {
            _spawners = spawners;
        }
        public void Pause()
        {
            _spawners.ForEach(x => x.Stop());
        }

        public void Resume()
        {
            _spawners.ForEach(x => x.Begin());
        }

        public void Tick()
        {
            if (!Input.GetKeyDown(KeyCode.B))
                return;
            
            Resume();
            Pause();
        }
    }
}