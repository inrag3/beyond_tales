using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;

namespace _Project.Runtime.Core.Interactables
{

    public abstract class Interactable : MonoBehaviour, ITransformable
    {
        public bool IsAccessible { get; protected set; } = true;
        public abstract void Interact(IInteractableVisitor visitor);
        public Transform Transform => transform;
    }
    
    
    
}