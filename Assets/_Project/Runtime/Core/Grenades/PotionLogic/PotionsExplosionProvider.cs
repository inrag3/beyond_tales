using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ModestTree;

namespace _Project.Runtime.Core.Grenades.PotionLogic
{
    public class PotionsExplosionProvider : IPotionExplosionProvider
    {
        private readonly List<GrenadeExplosion> _explosions = new();

        public bool IsAnyExplosionsActive => !_explosions.IsEmpty();
        public event Action<IReadOnlyList<GrenadeExplosion>> GrenadesUpdated;
        
        public void RemoveExplosionFromListAndUpdateTrigger(GrenadeExplosion explosion)
        {
            _explosions.Remove(explosion);
            GrenadesUpdated?.Invoke(new ReadOnlyCollection<GrenadeExplosion>(_explosions));
        }

        public void AddExplosionToListAndUpdateTrigger(GrenadeExplosion explosion)
        {
            _explosions.Add(explosion);
            GrenadesUpdated?.Invoke(new ReadOnlyCollection<GrenadeExplosion>(_explosions));
        }
    }
    
    public interface IPotionExplosionProvider
    {
        public event Action<IReadOnlyList<GrenadeExplosion>> GrenadesUpdated;

        public void AddExplosionToListAndUpdateTrigger(GrenadeExplosion explosion);

        public void RemoveExplosionFromListAndUpdateTrigger(GrenadeExplosion explosion);
        
        public bool IsAnyExplosionsActive { get; }
    }
}