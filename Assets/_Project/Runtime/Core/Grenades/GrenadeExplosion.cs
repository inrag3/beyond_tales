using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Grenades
{
    public class GrenadeExplosion : MonoBehaviour
    {
        public float Radius { get; set; } = 2;
        
        public Timer Timer { get; private set; }

        [Inject]
        private void Construct(Timer timer)
        {
            Timer = timer;
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}