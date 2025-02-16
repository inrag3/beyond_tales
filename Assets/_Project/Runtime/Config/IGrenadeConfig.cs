namespace _Project.Runtime.Config
{
    public interface IGrenadeConfig
    {
        public float GrenadeFrontForce { get; }
        public float GrenadeMaxDistance { get; }
        public float GrenadeThrowsTimeout { get; }
        public float GrenadeRecoveryTimeout { get; }
        public float GrenadeExplosionTimeout { get; }
        public float GrenadeTransformWorldRadius { get; }
        public float PotionExplosionRadius { get; }
        public float PotionExplosionDamage { get; }
        public float PotionPoisonDamage { get; }
        public float PotionPoisonTimeDelay { get; }
        public int PotionPoisonTimesCount { get; }
        
        public float PotionHealPoints { get; }
    }
}