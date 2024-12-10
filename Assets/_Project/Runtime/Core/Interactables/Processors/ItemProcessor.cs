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

        public void Processed(Item item)
        {
            ItemValue data = item.Value;
            _inventory.AddItem(data.ItemEnum, data.Value);
        }
    }
}