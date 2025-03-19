using UnityEngine;

namespace _Project.Runtime.Core.Grenades.Ingredients
{
    public abstract class CollectableIngredient : MonoBehaviour
    {
        public abstract PotionIngredients GetPotionIngredients { get; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IngredientCollector ingredientCollector))
            {
                ingredientCollector.AddIngredient(GetPotionIngredients);
                Destroy(gameObject);
            }
        }
    }
}