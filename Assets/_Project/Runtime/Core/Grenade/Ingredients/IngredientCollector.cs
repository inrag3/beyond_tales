using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Herbalist.Ingredients
{
    public class IngredientCollector : MonoBehaviour
    {
        private IPotionSelector _potionSelector;
        
        [Inject]
        private void Construct(IPotionSelector potionSelector)
        {
            _potionSelector = potionSelector;
        }

        public void AddIngredient(PotionIngredients ingredient)
        {
            _potionSelector.AddIngredient(ingredient);
        }
    }
}