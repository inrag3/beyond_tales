using System;
using System.Collections.Generic;
using _Project.Runtime.Core.Enemies;
using UnityEngine;

namespace _Project.Runtime.Core.Herbalist
{
    public class PoisonPotionCenter : GrenadeExplosion
    {
        private HashSet<Enemy> _poisonedEnemies = new();
        public event Action<Enemy> EnemyContacted;

        public void Start()
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }


        void OnTriggerEnter(Collider sight)
        {
            if (sight.TryGetComponent(out Enemy enemy))
            {
                Debug.Log("Found Enemy");
                if (!_poisonedEnemies.Contains(enemy))
                {
                    Debug.Log("subscribing on poisoning");
                    _poisonedEnemies.Add(enemy);
                    EnemyContacted?.Invoke(enemy);
                }
            }
        }
    }
}