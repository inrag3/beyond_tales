using System;
using System.Collections.Generic;
using _Project.Runtime.Infrastructure.Factories;
using ObservableCollections;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    [RequireComponent(typeof(SphereCollider))]
    public class Scanner<T> : MonoBehaviour, IScanner<T> where T : ITransformable
    {
        [SerializeField] private float _radius = 2f;
        [SerializeField] private SphereCollider _collider;
        private void OnValidate()
        {
            _collider.radius = _radius;
        }
        
        private readonly Dictionary<Collider, T> _colliders = new();
        
        private readonly ObservableHashSet<T> _components = new(20);
        private IHerbalistProvider _herbalistProvider;

        [Inject]
        public void Construct(IHerbalistProvider herbalistProvider)
        {
            _herbalistProvider = herbalistProvider;
        }

        public IObservableCollection<T> Components => _components;
        public bool IsEmpty => _components.Count == 0;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out T component))
                return;
            
            _colliders[other] = component;
            _components.Add(component);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_colliders.TryGetValue(other, out T component))
                return;

            _colliders.Remove(other);
            _components.Remove(component);
        }

        public T Get()
        {
            Vector3 at = _herbalistProvider.Herbalist.Transform.position;
            T closest = _components.Closest(at);
            return closest;
        }

        public void Remove(T component)
        {
            _components.Remove(component);
        }

        public T Get(Predicate<T> predicate)
        {
            Vector3 at = _herbalistProvider.Herbalist.Transform.position;
            T closest = _components.Closest(at, predicate);
            return closest;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_collider.transform.position, _radius);
        }
    }
    
}
