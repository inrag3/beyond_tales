namespace _Project.Runtime.Core.Interactables.Processors
{
    public interface IInteractableVisitor
    {
        public void Accept(Item item);
        public void Accept(Door door);
    }
}