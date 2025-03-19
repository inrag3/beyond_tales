using _Project.Runtime.Core.Interactables;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class Equipper : ITickable
    {
        private readonly Transform _hand;
        private readonly Transform _back;
        private readonly HerbalistAnimer _animer;
        private readonly IInputService _inputService;
        private Weapon _currentWeapon;

        public Equipper(IInputService inputService, HerbalistAnimer animer, Transform hand, Transform back)
        {
            _inputService = inputService;
            _animer = animer;
            _back = back;
            _hand = hand;
        }

        public void Tick()
        {
            if (_inputService.IsEquipButtonPressed)
            {
                Disequip();
            }
        }

        public void Equip(Weapon weapon)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.Drop();
            }
            
            _currentWeapon = weapon;
            _currentWeapon.Take(_hand);

            _currentWeapon.transform.SetParent(_hand);
            _currentWeapon.transform.localPosition = Vector3.zero;
            _currentWeapon.transform.localRotation = Quaternion.identity;
        }

        private void Disequip()
        {
            if (_currentWeapon == null)
                return;
            
            _currentWeapon.transform.SetParent(_back);
            _currentWeapon.transform.localPosition = Vector3.zero;
            _currentWeapon.transform.localRotation = Quaternion.identity;
            // _animer.PlayDisequip(() =>
            // {
            //     _currentWeapon.transform.SetParent(_back);
            //     _currentWeapon.transform.localPosition = Vector3.zero;
            //     _currentWeapon.transform.localRotation = Quaternion.identity;
            // });
        }
    }

}
