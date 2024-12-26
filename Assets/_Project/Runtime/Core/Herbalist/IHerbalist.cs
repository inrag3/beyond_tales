using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Interactables;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public interface IHerbalist : ITarget
    {
        public IHealth Health { get; }
        public IScanner<Interactable> Scanner { get; }
    }

    public interface ITarget : ITransformable, IDamageable
    {
    }

    public interface ITransformable
    {
        public Transform Transform { get; }
    }
}