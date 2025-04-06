using XNode;

namespace DialogueSystem.Nodes.Checks
{
    [CreateNodeMenu("Checks/BedPlanted")]
    [Node.NodeTintAttribute("#C12B00")]
    public class BedIsPlantedNode : CheckNode
    {
        public Bed Bed;
    }
}