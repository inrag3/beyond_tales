namespace _Project.Runtime.Config
{
    public interface IGrenadeConfig
    {
        public float GrenadeFrontForce { get; }
        public float GrenadeUpForce { get; }
        public float GrenadeThrowsTimeout { get; }
        public float GrenadeRecoveryTimeout { get; }
        public float GrenadeExplosionTimeout { get; }
    }
}