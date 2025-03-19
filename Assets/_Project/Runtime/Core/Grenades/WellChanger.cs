using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class WellChanger : SecondWorldExChangingTrigger
    {
        public override void TriggerWorldChange()
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        public override void TriggerWorldChangeBack()
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}