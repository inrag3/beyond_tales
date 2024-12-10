using _Project.Runtime.InventorySystem;
using UnityEngine;

namespace _Project.Runtime.Infrastructure.Factories
{
    public interface IFlowerFactory
    {
        public Flower Create(ItemEnum type);
    }
}