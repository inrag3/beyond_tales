using System;
using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class IngredientExChangeTrigger: SecondWorldExChangingTrigger
    {
        [SerializeField] private GameObject _original;
        [SerializeField] private GameObject _exchanged;

        private void Start()
        {
            _original.SetActive(true);
            _exchanged.SetActive(false);
        }

        protected override void TriggerWorldChange()
        {
            _original.SetActive(false);
            _exchanged.SetActive(true);
        }

        protected override void TriggerWorldChangeBack()
        {
            _original.SetActive(true);
            _exchanged.SetActive(false);
        }
    }
}