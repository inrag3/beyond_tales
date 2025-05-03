using UnityEngine;

namespace _Project.Runtime.Infrastructure.Factories
{
    public class EnemySpawnerStarter : MonoBehaviour
    {
        [SerializeField] private EnemySpawner spawner;

        private void Start()
        {
            Debug.Log(spawner);
            spawner.Begin();
        }
    }
}