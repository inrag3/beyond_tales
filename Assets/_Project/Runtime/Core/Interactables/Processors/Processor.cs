namespace _Project.Runtime.Core.Interactables.Processors
{
    public class Processor : IInteractableVisitor
    {
        private readonly ItemProcessor _itemProcessor;

        public Processor(ItemProcessor itemProcessor)
        {
            _itemProcessor = itemProcessor;
        }

        public void Accept(Item item)
        {
            _itemProcessor.Processed(item);
        }

        public void Accept(Door door)
        {
            
        }
    }
}



