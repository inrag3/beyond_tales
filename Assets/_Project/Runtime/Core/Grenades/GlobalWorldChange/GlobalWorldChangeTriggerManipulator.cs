using UnityEngine;

namespace _Project.Runtime.Core.Grenades.GlobalWorldChange
{
    public class GlobalWorldChangeTriggerManipulator: SecondWorldExChangingTrigger
    {
        [SerializeField] private GameObject globalWorldChangeTrigger;
        [SerializeField] private GameObject globalWorldChangeBackTrigger;

        private void Start()
        {
            globalWorldChangeBackTrigger.SetActive(false);
        }

        public override void TriggerWorldChange()
        {
            globalWorldChangeTrigger.gameObject.SetActive(false);
            globalWorldChangeBackTrigger.gameObject.SetActive(true);
            
        }

        public override void TriggerWorldChangeBack()
        {
            globalWorldChangeTrigger.gameObject.SetActive(true);
            globalWorldChangeBackTrigger.gameObject.SetActive(false);
        }
    }
}