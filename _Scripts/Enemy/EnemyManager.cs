using UnityEngine;

namespace MyShooter
{
    public class EnemyManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public GameObject enemy;
        public float spawnTime = 3f;
        public Transform[] spawnPoints;
        public Transform RootTransform;

        void Start()
        {
            InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
        }

        void Spawn()
        {
            if (playerHealth.Hp <= 0f) return;
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            var spawnedObject = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            spawnedObject.transform.parent = RootTransform;
        }
    }
}