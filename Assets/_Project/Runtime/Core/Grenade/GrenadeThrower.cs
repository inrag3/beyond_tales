using System;
using _Project.Runtime.Config;
using _Project.Runtime.Infrastructure.Factories;
using _Project.Runtime.InventorySystem;
using Zenject;

namespace _Project.Runtime.Core.Herbalist
{
    public class GrenadeThrower : IInitializable, ITickable, IDisposable

    {
        private bool _isRecoveringGrenades = false;
        private bool _readyToThrow = true;


        private readonly IHerbalistProvider _herbalistProvider;
        private readonly IInputService _inputService;

        private readonly IGrenadeConfig _grenadeConfig;
        private readonly Timer _throwCoolDownTimer;
        private readonly Timer _grenadeRecoveryTimer;
        private readonly IPlayerInventory _inventory;
        private readonly IItemContainer _itemContainer;
        private readonly IPotionApplierFactory _potionApplierFactory;
        
        

        public GrenadeThrower(
            IHerbalistProvider herbalistProvider,
            IInputService inputService,
            IGrenadeConfig grenadeConfig,
            IPlayerInventory inventory,
            IItemContainer itemContainer,
            IPotionApplierFactory potionApplierFactory,
            Timer throwCoolDownTimer,
            Timer grenadeRecoveryTimer
        )
        {
            _herbalistProvider = herbalistProvider;
            _inputService = inputService;
            _grenadeConfig = grenadeConfig;
            _inventory = inventory;
            _itemContainer = itemContainer;
            _potionApplierFactory = potionApplierFactory;
            _throwCoolDownTimer = throwCoolDownTimer;
            _grenadeRecoveryTimer = grenadeRecoveryTimer;
        }

        public void Initialize()
        {
            _throwCoolDownTimer.TimeEnded += ResetThrow;
            _grenadeRecoveryTimer.TimeEnded += RecoverGrenade;
        }

        public void Tick()
        {
            if (!(_inputService.IsGrenadeButtonPressed && _readyToThrow && _inventory.Items[ItemEnum.Grenade] > 0))
                return;
            _readyToThrow = false;
            _inventory.RemoveItem(ItemEnum.Grenade, 1);

            ThrowGrenade();

            _throwCoolDownTimer.Start(_grenadeConfig.GrenadeThrowsTimeout);


            if (!_isRecoveringGrenades)
            {
                _isRecoveringGrenades = true;
                _grenadeRecoveryTimer.Start(_grenadeConfig.GrenadeRecoveryTimeout);
            }
        }

        private void ThrowGrenade()
        {
            _potionApplierFactory.ApplyWorldChange(_inputService.Mouse,_herbalistProvider.Herbalist.Transform.position);
        }

        


        private void ResetThrow()
        {
            _readyToThrow = true;
        }

        private void RecoverGrenade()
        {
            _inventory.AddItem(ItemEnum.Grenade, 1);
            if (_inventory.Items[ItemEnum.Grenade] < _itemContainer.ItemData[ItemEnum.Grenade].MaxStackSize)
            {
                _grenadeRecoveryTimer.Start(_grenadeConfig.GrenadeRecoveryTimeout);
            }
            else
            {
                _isRecoveringGrenades = false;
            }
        }

        public void Dispose()
        {
            _throwCoolDownTimer.TimeEnded -= ResetThrow;
            _grenadeRecoveryTimer.TimeEnded -= RecoverGrenade;
        }
    }
}