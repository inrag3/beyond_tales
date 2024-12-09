using UnityEngine;

namespace DialogueSystem.Nodes.Checks
{
    [CreateNodeMenu("Checks/InventoryCheck")]
    [NodeTint("#F9DA6B")]
    public class InventoryCheckNode : CheckNode
    {
        [Tooltip("Предметы необходимые для прохождения проверки")]
        public ItemQuantityPair[] requiredItems;
    }
}