using System.Collections;
using Noko.Data;
using Noko.Enemy;
using UnityEngine;
using UnityEngine.Pool;

namespace Noko.Systems
{
    public class AreaSpawner : MonoBehaviour
    {
        [Header("Spawner Settings")]
        [SerializeField] private EnemySpawnerData spawnerConfig;
        [SerializeField] private float spawnRadius;

        private IObjectPool<EnemyAIBase> _enemyPool;
        private int _currentActiveEnemies;

        private void Awake()
        {
            _enemyPool = new ObjectPool<EnemyAIBase>(
                createFunc: () => Instantiate(spawnerConfig.enemyPrefab),
                actionOnGet: OnGetEnemy,
                actionOnRelease: OnReturnEnemy,
                actionOnDestroy: enemy => Destroy(enemy.gameObject),
                defaultCapacity: spawnerConfig.maxEnemiesInArea,
                maxSize: spawnerConfig.maxEnemiesInArea * 2
            );
        }

        private void Start()
        {
            for (int i = 0; i < spawnerConfig.maxEnemiesInArea; i++)
            {
                _enemyPool.Get();
            }
        }

        private void OnGetEnemy(EnemyAIBase enemy)
        {
            Vector3 randomPos = transform.position + (Random.insideUnitSphere * spawnRadius);
            randomPos.y = 0f;
            enemy.transform.position = randomPos;
            enemy.gameObject.SetActive(true);

            enemy.InitializeSpawner(this);
            enemy.ResetState(transform.position, spawnRadius);

            _currentActiveEnemies++;
        }

        private void OnReturnEnemy(EnemyAIBase enemy)
        {
            enemy.gameObject.SetActive(false);
            _currentActiveEnemies--;
            StartCoroutine(RespawnRoutine());
        }

        public void ReportEnemyDeath(EnemyAIBase enemy) => _enemyPool.Release(enemy);

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(spawnerConfig.respawnDelay);
            if (_currentActiveEnemies < spawnerConfig.maxEnemiesInArea) _enemyPool.Get();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}