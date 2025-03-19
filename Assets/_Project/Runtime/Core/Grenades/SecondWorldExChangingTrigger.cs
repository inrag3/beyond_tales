using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public abstract class SecondWorldExChangingTrigger : MonoBehaviour
    {
        public abstract void TriggerWorldChange();
        public abstract void TriggerWorldChangeBack();
    }
}