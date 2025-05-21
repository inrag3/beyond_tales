using System;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Interactables;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public interface IHerbalist : ITarget
    {
        public IHealth Health { get; }
        public IScanner<Interactable> Scanner { get; }

        public PlayerData PlayerData { get; }

        public void Teleport(Vector3 pos);

        public event Action OnDeath;
    }

    public interface ITarget : ITransformable, IDamageable
    {
    }

    public interface ITransformable
    {
        public Transform Transform { get; }
    }
}