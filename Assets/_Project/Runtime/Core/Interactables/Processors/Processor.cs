namespace _Project.Runtime.Core.Interactables.Processors
{
    public class Processor : IInteractableVisitor
    {
        private readonly ItemProcessor _itemProcessor;
        private readonly BedProcessor _bedProcessor;

        public Processor(ItemProcessor itemProcessor, BedProcessor bedProcessor)
        {
            _bedProcessor = bedProcessor;
            _itemProcessor = itemProcessor;
        }

        public void Accept(Item item)
        {
            _itemProcessor.Process(item);
        }

        public void Accept(Door door)
        {
            
        }

        public void Accept(Bed bed)
        {
            _bedProcessor.Process(bed);
        }
    }
}



