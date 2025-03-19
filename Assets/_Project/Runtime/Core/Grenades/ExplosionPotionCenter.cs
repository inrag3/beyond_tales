using _Project.Runtime.Config;
using _Project.Runtime.Core.Enemies;
using UnityEngine;
using Zenject;

namespace _Project.Runtime.Core.Grenades
{
    public class ExplosionPotionCenter : GrenadeExplosion
    {
        private IGrenadeConfig _grenadeConfig;

        [Inject]
        private void Inject(IGrenadeConfig grenadeConfig)
        {
            _grenadeConfig = grenadeConfig;
        }
        public void Start()
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            sight = gameObject.GetComponent<MeshCollider> ();
            Debug.Log(name + " started");

        }
        private MeshCollider sight;//; = this.gameObject.transform.FindChild ("SightRange").gameObject.GetComponent<MeshCollider> ();
        void OnTriggerEnter(Collider sight){
            Debug.Log ("Something Has Collided");
            if (sight.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_grenadeConfig.PotionExplosionDamage);
            }
            
        }
    }
}