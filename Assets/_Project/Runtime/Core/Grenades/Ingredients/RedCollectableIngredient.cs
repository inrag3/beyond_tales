namespace _Project.Runtime.Core.Grenades.Ingredients
{
    public class RedCollectableIngredient:CollectableIngredient
    {
        public override PotionIngredients GetPotionIngredients => new(1, 0, 0);
    }
}