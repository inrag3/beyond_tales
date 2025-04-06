using XNode;

namespace DialogueSystem.Nodes.Checks
{
    [CreateNodeMenu("Checks/BedPlantedCorrectly")]
    [Node.NodeTintAttribute("#C12B00")]
    public class BedIsPlantedCorrectlyNode : CheckNode
    {
        public Bed Bed;
    }
}