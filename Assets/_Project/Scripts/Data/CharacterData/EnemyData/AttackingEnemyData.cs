using UnityEngine;

namespace Noko.Data
{
    [CreateAssetMenu(fileName = "NewAttackingEnemyData", menuName = "Noko/Data/Enemy/Attacking")]
    public class AttackingEnemyData : EnemyData
    {
        public float attackRange;
        public float attackDamage;
        public float attackCooldown;
    }
}