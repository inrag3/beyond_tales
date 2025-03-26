using _Project.Runtime.Core.Herbalist;
using UnityEngine;

namespace _Project.Runtime.Core.Enemies
{
    public class DragonAss :MonoBehaviour, IDamageable
    {
        public void TakeDamage(float value)
        {
            Debug.Log("DragonAssSpawned");
        }
    }
}