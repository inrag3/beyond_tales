using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class DragonAssExchangeTrigger:SecondWorldExChangingTrigger
    {
        [SerializeField] GameObject _dragon;
        
        public override void TriggerWorldChange()
        {
            _dragon.SetActive(true);
        }

        public override void TriggerWorldChangeBack()
        {
            _dragon.SetActive(false);

        }
    }
}