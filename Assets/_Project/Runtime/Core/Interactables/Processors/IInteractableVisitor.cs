using _Project.Runtime.Core.Herbalist.GlobalWorldChange;
using _Project.Runtime.Core.Interactables.Items;

namespace _Project.Runtime.Core.Interactables.Processors
{
    public interface IInteractableVisitor
    {
        public void Accept(Item item);
        public void Accept(Door door);
        public void Accept(Bed bed);
        public void Accept(GlobalWorldChangeTrigger trigger);
        public void Accept(GlobalWorldChangeBackTrigger trigger);
    }
}