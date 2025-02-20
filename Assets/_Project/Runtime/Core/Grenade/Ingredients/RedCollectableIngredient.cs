using UnityEngine;

namespace _Project.Runtime.Core.Herbalist.Ingredients
{
    public class RedCollectableIngredient:CollectableIngredient
    {
        public override PotionIngredients GetPotionIngredients => new(1, 0, 0);
    }
}