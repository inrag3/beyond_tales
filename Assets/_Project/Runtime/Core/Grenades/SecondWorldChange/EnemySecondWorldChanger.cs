using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class EnemySecondWorldChanger : SecondWorldExChangingTrigger
    {
        private bool _isChanged = false;
        protected override void TriggerWorldChange()
        {
            if (!_isChanged)
            {
                //gameObject.GetComponent<Renderer>().material.color = Color.white;
                Debug.Log("Имеджинируем партиклы превращения в другое животное");
                gameObject.transform.localScale *= 0.5f;
                _isChanged = true;
            }
        }

        protected override void TriggerWorldChangeBack()
        {
        }
    }
}