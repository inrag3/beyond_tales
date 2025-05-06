using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class WellChanger : SecondWorldExChangingTrigger
    {
        protected override void TriggerWorldChange()
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        protected override void TriggerWorldChangeBack()
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}