using _Project.Runtime.Core.Interactables.Items;

namespace _Project.Runtime.Core.Interactables.Processors
{
    public class Processor : IInteractableVisitor
    {
        private readonly ItemProcessor _itemProcessor;
        private readonly BedProcessor _bedProcessor;
        private readonly DoorProcessor _doorProcessor;

        public Processor(ItemProcessor itemProcessor, BedProcessor bedProcessor, DoorProcessor doorProcessor)
        {
            _bedProcessor = bedProcessor;
            _itemProcessor = itemProcessor;
            _doorProcessor = doorProcessor;
        }

        public void Accept(Item item)
        {
            _itemProcessor.Process(item);
        }

        public void Accept(Door door)
        {
            _doorProcessor.Process(door);
        }

        public void Accept(Bed bed)
        {
            _bedProcessor.Process(bed);
        }
    }
}



