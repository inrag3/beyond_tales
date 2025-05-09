using System;
using System.Collections;
using _Project.Runtime.Core.Health;
using _Project.Runtime.Core.Herbalist;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Enemies
{
    public class Creature : MonoBehaviour, IDamageable, ITransformable
    {
        public event Action Hit;
        private Coroutine _coroutine;
        private Material _material;
        
        [Inject] 
        private void Construct(IHealth health)
        {
            Health = health;
            _material = gameObject.GetComponent<Renderer>().material;
            Debug.Log(_material);
        }
        public IHealth Health { get; private set; }
        public Transform Transform => transform;

        public virtual void TakeDamage(float value)
        {
            ColorOnDamage();
            Hit?.Invoke();
            Health.Decrease(value);
            if (Health.Value.CurrentValue == 0)
            {
                Die();
            }
        }

        private void ColorOnDamage()
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(ColorCoroutine());
        }

        private IEnumerator ColorCoroutine()
        {
            _material.color = Color.magenta;
            yield return new WaitForSeconds(1);
            _material.color = Color.white;
            yield return null;

        }

        protected virtual void Die()
        {
        }
    }
}