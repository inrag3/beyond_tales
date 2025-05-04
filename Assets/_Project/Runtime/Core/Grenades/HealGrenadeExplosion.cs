using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class HealGrenadeExplosion : GrenadeExplosion
    {
        public void Start()
        {
            gameObject.GetComponent<Renderer>().material.color = Color.magenta;
        }

        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}