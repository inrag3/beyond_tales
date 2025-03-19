using System;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;

namespace _Project.Runtime.Core.Interactables
{
    [RequireComponent(typeof(Rigidbody))]
    public class Weapon : Interactable
    {
        private Collider _collider;
        private Rigidbody _rigidbody;
        private void Awake()
        {
            _collider ??= GetComponent<Collider>();
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        public override void Interact(IInteractableVisitor visitor)
        {
            visitor.Accept(this);
        }

        public void Take(Transform destination)
        {
            transform.SetParent(destination);
            _collider.enabled = false;
            _rigidbody.isKinematic = true;
            IsAccessible = false;
            transform.localPosition = Vector3.zero;
            transform.transform.localRotation = Quaternion.identity;
        }

        public void Drop()
        {
            transform.SetParent(null);
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            IsAccessible = true;
        }
    }
}