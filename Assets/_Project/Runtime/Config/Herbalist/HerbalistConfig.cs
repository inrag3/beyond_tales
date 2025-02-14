using UnityEngine;

namespace _Project.Runtime.Config.Herbalist
{
    [CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/HealthConfig", order = 0)]
    public sealed class HerbalistConfig : ScriptableObject, IHealthConfig, ISpeedConfig, IGrenadeConfig, IPickerConfig
    {
        [field: SerializeField] public int MaxValue { get; private set; }

        [field: SerializeField] public int Speed { get; private set; }

        [field: Header("Grenade")]
        [field: SerializeField] public float GrenadeFrontForce { get; private set; }

        [field: SerializeField] public float GrenadeMaxDistance { get; private set; }
        [field: SerializeField] public float GrenadeThrowsTimeout { get; private set; }
        [field: SerializeField] public float GrenadeRecoveryTimeout { get; private set; }
        [field: SerializeField] public float GrenadeExplosionTimeout { get; private set; }

        [field: SerializeField] public float GrenadeTransformWorldRadius { get; private set; }
        [field: SerializeField] public float PotionHealPoints { get; private set; }


        [field: Header("Picker")]
        [field: SerializeField]
        public float Radius { get; private set; }
    }
}