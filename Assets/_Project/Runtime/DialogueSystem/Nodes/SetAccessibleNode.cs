using _Project.Runtime.Core.Interactables;
using XNode;

namespace DialogueSystem.Nodes
{
    [Node.NodeTintAttribute("#1010c1")]
    public class SetAccessibleNode : DialogueBaseNode
    {
        public Interactable Interactable;

        public bool IsAccessible;
    }
}