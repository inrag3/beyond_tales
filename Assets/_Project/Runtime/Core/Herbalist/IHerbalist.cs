using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public interface IHerbalist : IDamageable
    {
        public Transform Transform { get; }
    }
}