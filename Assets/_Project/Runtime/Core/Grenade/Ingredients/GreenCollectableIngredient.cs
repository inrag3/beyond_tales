using UnityEngine;

namespace _Project.Runtime.Core.Herbalist.Ingredients
{
    public class GreenCollectableIngredient:CollectableIngredient
    {
        public override PotionIngredients GetPotionIngredients => new(0, 1, 0);
    }
}