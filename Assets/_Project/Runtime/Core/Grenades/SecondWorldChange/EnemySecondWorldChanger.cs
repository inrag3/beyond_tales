using System;
using _Project.Runtime.Core.Enemies;
using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class EnemySecondWorldChanger : SecondWorldExChangingTrigger
    {
        private bool _isChanged = false;
        private float _sizeMod = 1.5f;
        private float _hpMod = 2f;
        [SerializeField] private GameObject obj;
        [SerializeField] private Creature _health;

        private Vector3 _startScale;

        private void Start()
        {
            _startScale = obj.transform.localScale;
        }

        protected override void TriggerWorldChange()
        {
            obj.gameObject.transform.localScale /= _sizeMod;
            var currentHealth = _health.Health.Value.CurrentValue;
            _health.Health.Decrease(currentHealth - currentHealth / _hpMod);
        }

        protected override void TriggerWorldChangeBack()
        {
            obj.gameObject.transform.localScale = _startScale;
            var currentHealth = _health.Health.Value.CurrentValue;
            _health.Health.Increase(currentHealth * _hpMod - currentHealth);
        }
    }
}