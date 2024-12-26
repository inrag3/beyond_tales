using _Project.Runtime.Infrastructure.Factories;

namespace _Project.Runtime.Core.Herbalist
{
    public class Planter
    {
        private readonly IFlowerFactory _flowerFactory;
    
        public Planter(IFlowerFactory flowerFactory)
        {
            _flowerFactory = flowerFactory;
        }
    
        public void Plant(Bed bed)
        {
            var flower = _flowerFactory.Create(bed.RequiredFlowerType);
            bed.Plant(flower);
        }
    }
}

