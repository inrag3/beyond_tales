using UnityEngine;

namespace DialogueSystem.Nodes.Checks
{
    [CreateNodeMenu("Checks/HealthCheck")]
    [NodeTint("#FF0011")]
    public class HealthCheck : CheckNode
    {
        [Tooltip("Нужно чтобы хп было ниже или выше")]
        public bool NeedLower;
        [Tooltip("Задаваемый порог")]
        public float Threshold;
    }
}