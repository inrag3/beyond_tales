using _Project.Runtime.Core.Herbalist;
using UnityEngine;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class Point : MonoBehaviour, ITransformable
    {
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = transform;
        }
    }
}