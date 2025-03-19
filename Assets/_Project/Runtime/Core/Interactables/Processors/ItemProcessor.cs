using _Project.Runtime.Core.Grenades.GlobalWorldChange;
using _Project.Runtime.Core.Herbalist;
using _Project.Runtime.Core.Interactables.Items;
using _Project.Runtime.InventorySystem;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Interactables.Processors
{
    public class ItemProcessor
    {
        private readonly IPlayerInventory _inventory;

        //Тут можно добавлять любые зависимости, просто регистрировать через Zenejct
        public ItemProcessor(IPlayerInventory inventory)
        {
            _inventory = inventory;
        }

        public void Process(Item item)
        {
            ItemValue data = item.Data;
            _inventory.AddItem(data.ItemEnum, data.Value);
        }
    }
    
    public class BedProcessor
    {
        private readonly IPlayerInventory _inventory;
        private readonly Planter _planter;

        public BedProcessor(IPlayerInventory inventory, Planter planter)
        {
            _planter = planter;
            _inventory = inventory;
        }

        public void Process(Bed bed)
        {
            ItemEnum requiredFlowerType = bed.RequiredFlowerType;
            if (_inventory.GetItemCount(requiredFlowerType) <= 0) 
                return;
            _inventory.RemoveItem(requiredFlowerType, 1);
            _planter.Plant(bed);
        }
    }

    public class DoorProcessor
    {

        public void Process(Door door)
        {
            door.SwitchState();
        }
    }

    public class GlobalWorldChangeProcessor
    {
        private readonly IGlobalWorldChangeProvider _provider;
        public GlobalWorldChangeProcessor(IGlobalWorldChangeProvider provider)
        {
            _provider = provider;
        }
        
        
        public void Process(GlobalWorldChangeTrigger trigger)
        {
            if (_provider.Activate(trigger.transform.position, trigger.Radius))
            {
                Debug.Log("World change triggered");
            }
            else
            {
                Debug.Log("World change cant be triggered");
            }
        }
        
        public void Process(GlobalWorldChangeBackTrigger trigger)
        {
            _provider.Deactivate();
            Debug.Log("World change back triggered");

        }
    }
}