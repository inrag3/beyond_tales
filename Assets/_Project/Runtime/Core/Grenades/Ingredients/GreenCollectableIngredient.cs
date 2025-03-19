namespace _Project.Runtime.Core.Grenades.Ingredients
{
    public class GreenCollectableIngredient : CollectableIngredient
    {
        public override PotionIngredients GetPotionIngredients => new(0, 1, 0);
    }
}