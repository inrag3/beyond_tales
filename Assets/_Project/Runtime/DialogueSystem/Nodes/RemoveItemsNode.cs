using UnityEngine;

namespace DialogueSystem.Nodes
{
    [CreateNodeMenu("InventoryActions/RemoveItems")]
    [NodeTint("#E5284E")]
    public class RemoveItemsNode : DialogueBaseNode
    {
        [Tooltip("Список предметов на удаление")]
        public ItemQuantityPair[] itemsToRemove;
    }
}