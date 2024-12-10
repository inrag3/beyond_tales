using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Interactables;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public interface IHerbalist : IDamageable, ITransformable
    {
        public IHealth Health { get; }
        public IScanner<Interactable> Scanner { get; }
    }

    public interface ITransformable
    {
        public Transform Transform { get; }
    }
}