using System;
using System.Collections.Generic;
using _Project.Runtime.Core.Enemies;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.Interactables.Processors;
using UnityEngine;

namespace _Project.Runtime.Core.Interactables
{
    [RequireComponent(typeof(Rigidbody))]
    public class Weapon : Interactable
    {
        private Collider _collider;
        private Rigidbody _rigidbody;
        private Attacker _attacker;
        private void Awake()
        {
            CheckRigidBody();
        }

        public void CheckRigidBody()
        {
            _collider ??= GetComponent<Collider>();
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        public override void Interact(IInteractableVisitor visitor)
        {
            visitor.Accept(this);
        }

        public void Take(Transform destination, Attacker attacker)
        {
            transform.SetParent(destination);
            // _collider.enabled = false;
            _rigidbody.isKinematic = true;
            IsAccessible = false;
            transform.localPosition = Vector3.zero;
            transform.transform.localRotation = Quaternion.identity;
            _attacker = attacker;
        }

        public void Drop()
        {
            transform.SetParent(null);
            // _collider.enabled = true;
            _rigidbody.isKinematic = false;
            IsAccessible = true;
            _attacker = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Creature creature))
            {
                _attacker?.AddAttackedCreature(creature);

            }
        }
    }
}