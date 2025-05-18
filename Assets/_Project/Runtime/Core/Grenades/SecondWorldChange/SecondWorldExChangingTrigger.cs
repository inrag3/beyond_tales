using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public abstract class SecondWorldExChangingTrigger : MonoBehaviour
    {
        protected int _potionsCount = 0;

        public void TryTriggerWorldChange()
        {
            if (_potionsCount == 0)
                TriggerWorldChange();
            _potionsCount++;
        }

        public void TryTriggerWorldChangeBack()
        {
            _potionsCount--;
            if (_potionsCount == 0)
                TriggerWorldChangeBack();
        }

        protected abstract void TriggerWorldChange();
        protected abstract void TriggerWorldChangeBack();
    }
}