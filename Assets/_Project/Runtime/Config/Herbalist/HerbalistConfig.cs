using UnityEngine;

namespace _Project.Runtime.Config.Herbalist
{
    [CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/HealthConfig", order = 0)]
    public sealed class HerbalistConfig : ScriptableObject, IHealthConfig, ISpeedConfig
    {
        [field: SerializeField] public int MaxValue { get; private set; }

        [field: SerializeField] public int Speed { get; private set; }
    }
}