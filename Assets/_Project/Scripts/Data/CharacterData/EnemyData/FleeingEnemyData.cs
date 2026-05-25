using UnityEngine;

namespace Noko.Data
{
    [CreateAssetMenu(fileName = "NewFleeingEnemyData", menuName = "Noko/Data/Enemy/Fleeing")]
    public class FleeingEnemyData : EnemyData
    {
        public float fleeSpeedMultiplier;
    }
}