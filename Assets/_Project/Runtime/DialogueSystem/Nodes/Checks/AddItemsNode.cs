using UnityEngine;

namespace DialogueSystem.Nodes.Checks
{
    [CreateNodeMenu("InventoryActions/AddItems")]
    public class AddItemsNode : CheckNode
    {
        [Tooltip("Предметы, которые нужно добавить в инвентарь")]
        public ItemQuantityPair[] itemsToAdd;
        [Tooltip("Должны ли эти предметы быть добавлены если это возможно или же стоит просто проверить возможность" +
                 "их добавления")]
        public bool AddIfPossible=true;
    }
}