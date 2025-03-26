using System;
using _Project.Runtime.Core.Enemies;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class Detector : IInitializable, ITickable, IGizmoDrawer
    {
        private readonly Transform _transform;
        private readonly IInputService _inputService;
        private LayerMask _layerMask;
        private Vector3 _direction;
        public Detector(Transform transform, IInputService inputService, GizmosDrawer drawer)
        {
            _inputService = inputService;
            _transform = transform;
            drawer.Register(this);
        }

        public Creature Target { get; private set; }

        public void Initialize()
        {
            _layerMask = 1 << 7;
        }
        
        public void Tick()
        {
            _direction = _inputService.Mouse - _transform.position;
            _direction.Normalize();
            if (!Physics.SphereCast(_transform.position, 0.5f, _direction, out RaycastHit info, 2f, _layerMask))
            {
                Target = null;
                return;
            }
                

            Collider collider = info.collider;
            if (!collider.TryGetComponent(out Creature enemy))
                return;
            Target = enemy;
        }
        public void Draw()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_transform.position, _direction*2f);
            if(Target != null)
                Gizmos.DrawSphere(Target.transform.position, .5f);
        }
    }

    public interface IGizmoDrawer
    {
        public void Draw();
    }
}