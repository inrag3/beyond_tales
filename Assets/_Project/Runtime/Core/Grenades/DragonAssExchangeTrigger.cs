using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class DragonAssExchangeTrigger:SecondWorldExChangingTrigger
    {
        [SerializeField] GameObject _dragon;
        
        protected override void TriggerWorldChange()
        {
            _dragon.SetActive(true);
        }

        protected override void TriggerWorldChangeBack()
        {
            _dragon.SetActive(false);

        }
    }
}