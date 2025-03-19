namespace _Project.Runtime.Core.Grenades.Ingredients
{
    public class BlueCollectableIngredient : CollectableIngredient
    {
        public override PotionIngredients GetPotionIngredients => new(0, 0, 1);
    }
}