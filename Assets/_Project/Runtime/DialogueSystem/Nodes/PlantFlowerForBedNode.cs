using _Project.Runtime.InventorySystem;
using UnityEngine;

namespace DialogueSystem.Nodes
{
    [NodeTint("#C12B00")]
    public class PlantFlowerForBedNode : DialogueBaseNode
    {
        [Tooltip("Ссылка на кровать на которой будет выращен цветок")]
        public Bed Bed;

        public ItemEnum FlowerType;
    }
}