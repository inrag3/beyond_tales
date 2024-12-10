using System.Collections.Generic;
using _Project.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class Scanner<T> : IScanner<T>, ITickable where T : ITransformable
    {
        private const float RADIUS = 2f;
        private readonly Collider[] _colliders = new Collider[20];
        private readonly IHerbalistProvider _herbalistProvider;
        private readonly Dictionary<T, Collider> _cache = new();
        private readonly HashSet<Collider> _cacheColliders = new();
        public Scanner(IHerbalistProvider herbalistProvider)
        {
            _herbalistProvider = herbalistProvider;
        }
        public bool IsEmpty => _cache.Count == 0;
        
        public void Tick()
        {
            int size = Physics.OverlapSphereNonAlloc(_herbalistProvider.Herbalist.Transform.position, RADIUS, _colliders);
            for (var i = 0; i < size; i++)
            {
                Collider collider = _colliders[i];
                if (_cacheColliders.Contains(collider))
                    continue;
                
                if (!collider.TryGetComponent(out T component))
                    continue;
                
                _cacheColliders.Add(collider);
                _cache.Add(component, collider);
            }
        }

        public T Get()
        {
            var keys = _cache.Keys;
            var closest = keys.Closest(_herbalistProvider.Herbalist.Transform.position);
            var collider = _cache[closest];
            _cacheColliders.Remove(collider);
            _cache.Remove(closest);
            return closest;
        }
    }
}
