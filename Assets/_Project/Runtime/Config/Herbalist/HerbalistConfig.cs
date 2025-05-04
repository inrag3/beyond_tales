using UnityEngine;

namespace _Project.Runtime.Config.Herbalist
{
    [CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/HealthConfig", order = 0)]
    public sealed class HerbalistConfig : ScriptableObject, IHealthConfig, ISpeedConfig, IGrenadeConfig, IPickerConfig, ILootConfig, IAttackConfig
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
        [field: Header("Healing potion")]
        [field: SerializeField] public float PotionHealPoints { get; private set; }
        [field: Header("Explotion potion")]

        [field: SerializeField] public float PotionExplosionRadius { get; private set; }
        [field: SerializeField] public float PotionExplosionDamage { get; private set; }
        [field: Header("Poison potion")]
        [field: SerializeField] public float PotionPoisonDamage { get; private set; }
        [field: SerializeField] public float PotionPoisonTimeDelay { get; private set; }
        [field: SerializeField] public int PotionPoisonTimesCount { get; private set; }
        
        [field: Header("HerbalistAttack")]
        [field: SerializeField] public float HerbalistDamage { get; private set; }

        [field: Header("Picker")]
        [field: SerializeField]
        public float Radius { get; private set; }
        
        [field: Header("Loot")]
        [field: SerializeField] public int MaxLootCount { get; private set;}
        [field: SerializeField] public int LootMinDistance { get; private set;}
        [field: SerializeField] public int LootMaxDistance { get; private set;}
        [field: SerializeField] public int LootFrontForce { get; private set;}
        [field: SerializeField] public string LootRedPath { get; private set;}
        [field: SerializeField] public string LootGreenPath { get; private set;}
        [field: SerializeField] public string LootBluePath { get; private set;}
    }
}