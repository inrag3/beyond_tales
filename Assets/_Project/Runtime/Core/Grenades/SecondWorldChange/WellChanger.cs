using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class WellChanger : SecondWorldExChangingTrigger
    {
        [SerializeField] private GameObject _goodWell;
        [SerializeField] private GameObject _badWell;

        void Start()
        {
            _badWell.SetActive(true);
            _goodWell.SetActive(false);
        }
        
        protected override void TriggerWorldChange()
        {
            _badWell.SetActive(false);
            _goodWell.SetActive(true);
        }

        protected override void TriggerWorldChangeBack()
        {
            _badWell.SetActive(true);
            _goodWell.SetActive(false);
        }
    }
}