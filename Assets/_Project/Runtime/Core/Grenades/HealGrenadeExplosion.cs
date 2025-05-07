using UnityEngine;

namespace _Project.Runtime.Core.Grenades
{
    public class HealGrenadeExplosion : GrenadeExplosion
    {
        public void Start()
        {
            gameObject.GetComponent<Renderer>().material.color = Color.magenta;
            gameObject.transform.position = new Vector3(00f, 0.04f, 0.0f);
            gameObject.transform.localScale = new Vector3(0.45f, 0.3f, 0.45f);
        }

        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}