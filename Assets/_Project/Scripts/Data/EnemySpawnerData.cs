using UnityEngine;
using Noko.Enemy;

namespace Noko.Data
{
    [CreateAssetMenu(fileName = "NewEnemySpawnerData", menuName = "Noko/Data/Enemy Spawner Data")]
    public class EnemySpawnerData : ScriptableObject
    {
        public EnemyAIBase enemyPrefab;
        public int maxEnemiesInArea;
        public float respawnDelay;
    }
}