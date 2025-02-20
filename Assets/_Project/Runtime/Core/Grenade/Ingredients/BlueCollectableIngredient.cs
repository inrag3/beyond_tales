using System;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist.Ingredients
{
    public class BlueCollectableIngredient : CollectableIngredient
    {
        public override PotionIngredients GetPotionIngredients => new(0, 0, 1);
    }
}